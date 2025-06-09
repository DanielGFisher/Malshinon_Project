using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon_Project.DAL
{
    public class DAL
    {
        private string ConnectionString = "server=localhost;user=root;password='';database=Malshinon";
        private MySqlConnection _connection;

        public MySqlConnection OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(ConnectionString);
            }
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
                Console.WriteLine("Connected successfully!");
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public MySqlConnection Connection()
        {
            return _connection;
        }
    }
}
