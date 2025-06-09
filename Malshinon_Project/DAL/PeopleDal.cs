using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data.MySqlClient;
using Malshinon_Project.Models;

namespace Malshinon_Project.DAL
{
    internal class PeopleDal
    {
        private DAL Dal = new DAL();
        public PeopleDal()
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

        public Person SearchUniqueCode(string uniqueCode)
        {
            string query = "SELECT * FROM People WHERE secretCode = @uniqueCode";
            
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    string firstName = reader.GetString("firstName");
                    string lastName = reader.GetString("lastName");
                    string secretCode = reader.GetString("secretCode");
                    string type = reader.GetString("type");
                    int numReports = reader.GetInt32("numReports");
                    int numMentions = reader.GetInt32("numMentions");

                    Person person = new Person(firstName, lastName, secretCode, type, numReports, numMentions);
                    return person;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                Dal.CloseConnection();
            }
            return null;
        }
    }
}
