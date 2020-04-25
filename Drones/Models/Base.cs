using Microsoft.IdentityModel.Protocols;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;


namespace Drones.Models
{
    public class Base
    {
        protected MySqlConnection connection;

        public Base()
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            connection = new MySqlConnection(conn);
        }

        protected bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                string msg = ex.Message;
                switch (ex.Number)
                {
                    case 0:
                        msg = "Cannot connect to server";
                        break;

                    case 1045:
                        msg = "Invalid username/password";
                        break;
                }
                Debug.Print("ERROR: " + msg);
                return false;
            }
        }

        protected bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Debug.Print("ERROR: " + ex.Message);
                return false;
            }
        }
    }

}
