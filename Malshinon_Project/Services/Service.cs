using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Services
{
    public class Service
    {
        public static string GetUniqueCode()
        {
            try
            {
                Console.WriteLine("Please enter your UniqueCode: ");
                string UniqueCode = Console.ReadLine();
                return UniqueCode;
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

        public static string GenerateRandomString()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";
            Random random = new Random();

            for (int i = 0; i < 12 ; i++)
            {
                result += chars[random.Next(0,62)];
            }

            return result;
        }
    }
}
