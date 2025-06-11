using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Services
{
    public static class Logger
    {
        public static string logFilePath = @"..\..\..\log.txt";

        public static void WriteLog(string message)
        {
            try
            {

                string logEntry = $"{DateTime.Now}: {message}\n";
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
}
