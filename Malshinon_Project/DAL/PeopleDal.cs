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

        public string AddNewPerson(Person person)
        {
            string query = $"INSERT INTO People(FirstName, LastName) " +
                $"VALUES(@firstName, @lastName);";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"firstName", person.FirstName);
                cmd.Parameters.AddWithValue(@"lastName", person.LastName);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding new person: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
            string log = $"New Person;\n" +
                $"Name - {person.FirstName + " " + person.LastName}\n" +
                $"Secret-Code - {person.ShowSecretCode}\n" +
                $"Type - {person.Type}\n" +
                $"Report Count - {person.NumReports}\n" +
                $"Mention Count - {person.NumMentions}\n";
            return log;
        }

        public int ReturnPersonID(string uniqueCode)
        {
            string query = "SELECT * FROM People" +
                " WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            int id = 0;
            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id = reader.GetInt32("id");
                    return id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Retrieval error: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                Dal.CloseConnection();
            }
            return id;
        }

        public void IncrementNumReports(string uniqueCode)
        {
            string query = $"UPDATE people " +
                $"SET numReports = reportNum + 1" +
                $" WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
        }

        public void IncrementNumMentions(string uniqueCode)
        {
            string query = $"UPDATE people " +
                $"SET numMentions = reportNum + 1 " +
                $"WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
        }

        public int CountUserReports(string uniqueCode)
        {
            string query = "SELECT numReports " +
                "FROM People" +
                "WHERE secretCode = @uniqueCode;";
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            int count = 0;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    count = Convert.ToInt32(result);
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

        public int CountUserMentions(string uniqueCode)
        {
            string query = "SELECT numMentions" +
                "FROM People" +
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

        public string UpdateStatusPotential(string uniqueCode)
        {
            string query = "UPDATE People" +
                "SET type = 'potential agent'" +
                "WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
            string log = $"Person with Secret-Code {uniqueCode} status changed to potential agent";
            return log;
        }

        public string UpdateStatusThreat(string uniqueCode)
        {
            string query = "UPDATE People" +
                "SET type = 'potential threat'" +
                "WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
            finally
            {
                Dal.CloseConnection();
            }
            string log = $"Person with Secret-Code {uniqueCode} status changed to potential threat";
            return log;
        }
    }
}