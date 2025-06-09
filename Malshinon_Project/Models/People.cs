using Malshinon_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string SecretCode;
        public string Type {get; set;}
        public int NumReports = 0;
        public int NumMentions = 0;

        public Person(string firstName, string lastName,string secretCode, string type, int numReports, int numMentions)
        {
            FirstName = firstName;
            LastName = lastName;
            SecretCode = secretCode;
            Type = type;
            NumReports = numReports;
            NumMentions = numMentions;
        }
    }
}
