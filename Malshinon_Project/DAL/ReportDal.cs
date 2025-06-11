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

        public string InsertNewReport(int reporterid, int targetid, string text)
        {
            string query = "INSERT INTO reports(reporterid, targetid, documentation) " +
                            "VALUES(@reporterID, @targetID, @documentation);";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"reporterID", reporterid);
                cmd.Parameters.AddWithValue(@"targetID", targetid);
                cmd.Parameters.AddWithValue(@"documentation", text);

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
                "FROM reports " +
                "INNER JOIN People ON people.id = reports.id " +
                "WHERE secretCode = @uniqueCode;";
            
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            int count = 0;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    count = reader.GetInt32("AverageLength");
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
            return count;
        }
    }
}
