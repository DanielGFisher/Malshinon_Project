using Malshinon_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Services
{
    public class Service
    {
        // Gets name from user and returns it
        public static string GetUniqueCode()
        {
                Console.WriteLine("Please enter your UniqueCode: ");
                string UniqueCode = Console.ReadLine();
                return UniqueCode;
        }

        // Creates randomised code and returns it
        public static string GenerateRandomString()
        {
            string randomString = Guid.NewGuid().ToString();
            string code = randomString.Substring(0);
            return randomString;
        }

        // Creates Reporter Person Object
        public static Person CreateReporter(string firstName, string lastName)
        {
            Person reporter = new Person(firstName, lastName);
            reporter.NumMentions = 0;
            reporter.NumReports = 1;
            reporter.InsertIntoSecretCode();
            reporter.Type = "reporter";

            return reporter;
        }

        // Creates Target Person Object
        public static Person CreateTarget(string firstName, string lastName)
        {
            Person target = new Person(firstName, lastName);
            target.NumMentions = 1;
            target.NumReports = 0;
            target.InsertIntoSecretCode();
            target.Type = "target";

            return target;
        }

        // Retrieves report and returns as string
        public static string RetrieveReport()
        {
            Console.WriteLine("Enter a Report: ");
            string textBlog = Console.ReadLine();
            return textBlog;
        }

        // Checks reports threshold
        public Boolean HasReports(int reportCount)
        {
            if (reportCount >= 10) return true;
            else return false;
        }

        // Checks average text and report thresholds for potential and returns boolean
        public static bool PotentialAgentVerification(int reportCount, int averageReportLength)
        {
            if (reportCount >= 10 && averageReportLength >= 100) return true;
            else return false;
        }

        // Checks mention thresholds for threat and returns boolean
        public static bool PotentialThreatVerification(int mentionCount)
        {
            if (mentionCount >= 20) return true;
            else return false;
        }
    }
}
