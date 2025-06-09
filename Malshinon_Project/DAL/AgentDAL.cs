using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon_Project.DAL
{
    internal class AgentDAL
    {
        private DAL Dal = new DAL();
        public AgentDAL()
        {
            try
            {
                Dal.OpenConnection();
            }
            catch (MySqlException ex)
            {

            }
        }
    }
}
