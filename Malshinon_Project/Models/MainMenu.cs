using Malshinon_Project.DAL;
using Malshinon_Project.Services;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using MySqlX.XDevAPI.CRUD;
using MySqlX.XDevAPI.Common;

namespace Malshinon_Project.Models
{
    public class Menu
    {
        public PeopleDal peopleDal = new PeopleDal();
        public ReportDal reportDal = new ReportDal();
        
        public void MainMenu()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Menu:\n" +
                    "1 - Identification:\nPut in your Secret-Code\n\n" +
                    "2 - Create Account:\nStart your account and recieve a Secret-Code\n\n" +
                    "3 - Submit Report:\nSubmit a report on a target\n\n" +
                    "4 - Exit:\nExit the program\n\n" +
                    "Selection: ");

                int selection = int.Parse(Console.ReadLine());
                switch (selection)
                {
                    case 1:
                        PersonIdentificationFlow();
                        break;

                    case 2:
                        CreateReporter();
                        break;

                    case 3:
                        IntelSubmissionFlow();
                        break;

                    case 4:
                        Console.WriteLine("Goodbye");
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        public void PersonIdentificationFlow()
        {
            string uniqueCode = Service.GetUniqueCode();
            int userID = peopleDal.ReturnPersonID(uniqueCode);
            
            if (userID == 0) CreateReporter();
            else Console.WriteLine("Exists in DB");
        }

        public void IntelSubmissionFlow()
        {
            string report = Service.RetrieveReport();
            Console.WriteLine("Reporter Code:");
            string reporterCode = Service.GetUniqueCode();
            Console.WriteLine("Target Code");
            string targetCode = Service.GetUniqueCode();

            int reporterID = peopleDal.ReturnPersonID(reporterCode);

            int targetID = peopleDal.ReturnPersonID(targetCode);

            if (targetID > 0 && reporterID > 0)
            {
                string log = reportDal.InsertNewReport(reporterID, targetID, report);
                Logger.WriteLog(log);
            }
            else if (targetID == 0 && reporterID == 0)
            {
                Console.WriteLine("Target Name:");
                Person target = CreateTarget();
                targetCode = target.ShowSecretCode();
                targetID = peopleDal.ReturnPersonID(targetCode);

                Console.WriteLine("Reporter Name:");
                Person reporter = CreateReporter();
                reporterCode = reporter.ShowSecretCode();
                reporterID = peopleDal.ReturnPersonID(reporterCode);
                string log = reportDal.InsertNewReport(reporterID, targetID, report);
                Logger.WriteLog(log);
            }
            else if (targetID == 0)
            {
                Console.WriteLine("Target Name:");
                Person user = CreateTarget();
                targetCode = user.ShowSecretCode();
                targetID = peopleDal.ReturnPersonID(targetCode);

                string log = reportDal.InsertNewReport(reporterID, targetID, report);
                Logger.WriteLog(log);
            }
            else if (reporterID == 0)
            {
                Console.WriteLine("Reporter Name:");
                Person user = CreateReporter();
                reporterCode = user.ShowSecretCode();
                reporterID = peopleDal.ReturnPersonID(reporterCode);
                string log = reportDal.InsertNewReport(reporterID, targetID, report);
                Logger.WriteLog(log);
            }
            peopleDal.IncrementNumMentions(targetCode);
            peopleDal.IncrementNumReports(reporterCode);

            if (peopleDal.CountUserMentions(targetCode) > 0 && peopleDal.CountUserReports(targetCode) > 0)
            {
                string log = peopleDal.UpdateStatusBoth(targetCode);
                Logger.WriteLog(log);
            }

            if (peopleDal.CountUserMentions(reporterCode) > 0 && peopleDal.CountUserReports(reporterCode) > 0)
            {
                string log = peopleDal.UpdateStatusBoth(reporterCode);
                Logger.WriteLog(log);
            }

            if (Service.PotentialAgentVerification(peopleDal.CountUserReports(reporterCode), reportDal.AverageTextSize(reporterCode)))
            {
                string log = peopleDal.UpdateStatusPotential(reporterCode);
                Logger.WriteLog(log);
            }

            if (Service.PotentialThreatVerification(peopleDal.CountUserMentions(targetCode)))
            {
                string log = peopleDal.UpdateStatusThreat(targetCode);
                Logger.WriteLog(log);
            }
        }

        public Person CreateReporter()
        {
            try
            {
                Console.WriteLine("Please enter first name (and any middle names): ");
                string firstName = Console.ReadLine();
            
                Console.WriteLine("Please enter family name: ");
                string lastName = Console.ReadLine();
            
                Person newUser = peopleDal.AddNewPerson(Service.InitialiseReporter(firstName, lastName));
                Logger.WriteLog(Service.CreateNewPersonReport(newUser));
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem executing: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Still Running...");
            }
            return null;
        }

        public Person CreateTarget()
        {
            try
            {
                Console.WriteLine("Please enter first name (and any middle names): ");
                string firstName = Console.ReadLine();

                Console.WriteLine("Please enter family name: ");
                string lastName = Console.ReadLine();

                Person newUser = peopleDal.AddNewPerson(Service.InitialiseReporter(firstName, lastName));
                Logger.WriteLog(Service.CreateNewPersonReport(newUser));
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem executing: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Still Running...");
            }
            return null;
        }
    }
}