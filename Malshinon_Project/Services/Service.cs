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
            return randomString;
        }

        // Creates Person Object
        public static Person CreateReporter(string firstName, string lastName)
        {
            Person reporter = new Person(firstName, lastName);
            reporter.NumMentions = 0;
            reporter.NumReports = 1;
            reporter.InsertIntoSecretCode();
            reporter.Type = "reporter";

            return reporter;
        }
    }
}
