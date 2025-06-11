using Malshinon_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Malshinon_Project.DAL
{
    public class PeopleDal
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

        public Person AddNewPerson(Person person)
        {
            string query = $"INSERT INTO people(FirstName, LastName, SecretCode, Type, NumReports, NumMentions) " +
                $"VALUES(@firstName, @lastName, @secretCode, @type, @numReports, @numMentions);";
            Dal.OpenConnection();
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"firstName", person.FirstName);
                cmd.Parameters.AddWithValue(@"lastName", person.LastName);
                cmd.Parameters.AddWithValue(@"secretCode", person.ShowSecretCode());
                cmd.Parameters.AddWithValue(@"Type", person.Type);
                cmd.Parameters.AddWithValue(@"NumReports", person.NumReports);
                cmd.Parameters.AddWithValue(@"NumMentions", person.NumMentions);

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
            return person;
        }

        public int ReturnPersonID(string uniqueCode)
        {
            string query = "SELECT id FROM people " +
                "WHERE secretCode = @uniqueCode";
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

        public string ReturnPersonType(string uniqueCode)
        {
            string query = "SELECT type FROM people " +
                "WHERE secretCode = @uniqueCode";
            Dal.OpenConnection();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            string type = null;
            try
            {
                cmd = new MySqlCommand(query, Dal.Connection());
                cmd.Parameters.AddWithValue(@"uniqueCode", uniqueCode);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    type = reader.GetString("type");
                    return type;
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
            return type;
        }


        public void IncrementNumReports(string uniqueCode)
        {
            string query = $"UPDATE people " +
                $"SET numReports = numReports + 1 " +
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

        public void IncrementNumMentions(string uniqueCode)
        {
            string query = $"UPDATE people " +
                $"SET numMentions = numMentions + 1 " +
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
                "FROM People " +
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
                    count = reader.GetInt32("numReports");
                }
                return count;
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
            string query = "SELECT numMentions " +
                "FROM People " +
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
                    count = reader.GetInt32("numMentions");
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

        public string UpdateStatusPotential(string uniqueCode)
        {
            string query = "UPDATE people " +
                "SET type = 'potential agent' " +
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
            string query = "UPDATE people " +
                "SET type = 'potential threat' " +
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

        public string UpdateStatusBoth(string uniqueCode)
        {
            string query = "UPDATE people " +
                "SET type = 'Both' " +
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
            string log = $"Person with Secret-Code {uniqueCode} status changed to Both";
            return log;
        }
    }
}