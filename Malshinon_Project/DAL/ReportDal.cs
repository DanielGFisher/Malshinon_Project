using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Malshinon_Project.Models;
using Malshinon_Project.Services;

namespace Malshinon_Project.DAL
{
    public class ReportDal
    {
        private DAL Dal = new DAL();
        public ReportDal()
        {
            try
            {
                Dal.OpenConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
        }

        public string InsertNewReport(string reporterid, string targetid, string text)
        {
            string query = "INSERT INTO Reports(reporterid, targetid, documentation VALUES(@reporterID, @targetID, @Documentation);";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"reporterID", reporterid);
                cmd.Parameters.AddWithValue(@"targetID", targetid);
                cmd.Parameters.AddWithValue(@"Documentation", text);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting to table: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
            string log = $"New Report;\n" +
                $"Reporter ID - {reporterid}\n" +
                $"Target ID - {targetid}\n" +
                $"Report Document - {text}";
            return log;
        }

        public int AverageTextSize(string uniqueCode)
        {
            string query = "Select AVG(LENGTH(documentation)) AS AverageLength  " +
                "FROM Reports " +
                "INNER JOIN People ON People.id = Reports.id " +
                "WHERE secretCode = @uniqueCode;";
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            int average = 0;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    average = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Executing cmd: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
            return average;
        }
    }
}
