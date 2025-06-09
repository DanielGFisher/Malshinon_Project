using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Models
{
    public class People
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string SecretCode = SecretCodeInitializer();
        public string Type {get; set;}
        public int NumReports = 0;
        public int NumMentions = 0;

        public People(string firstName, string lastName, string type)
        {
            FirstName = firstName;
            LastName = lastName;
            Type = type;
        }
    }
}
