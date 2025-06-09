using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Services
{
    public class Services
    {
        public static string GetName()
        {
            try
            {
                Console.WriteLine("Please enter your full name (first and family): ");
                string inputName = Console.ReadLine();
                return inputName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid Input: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Running regardless");
            }
            return null;
        }

        public static string IdentifyFirstName(string inputName)
        {
            try
            {
                int spaceIndex = inputName.IndexOf(' ');
                string firstName = "";

                if (spaceIndex > -1) firstName = inputName.Substring(0, spaceIndex);
                return firstName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid Operation: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Running regardless");
            }
            return null;
        }
    }
}
