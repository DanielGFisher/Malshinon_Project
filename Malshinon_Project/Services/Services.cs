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
                Console.WriteLine("Please enter your name: ");
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
        }
    }
}
