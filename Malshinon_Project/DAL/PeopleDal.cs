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

                    Person person = new Person(firstName, lastName);
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

        public Person AddNewPerson(Person person)
        {
            string query = $"INSERT INTO People(FirstName, LastName) VALUES(@firstName, @lastName);";
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
            return person;
        }
    }
}
