using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Drones.Models
{
    public class Route
    {
        public int id { get; private set; }
        public double height { get; private set; }

        public Route()
        {
        }

        public Route(double height)
        {
            this.height = height;
        }

        public static void Create(Route route)
        {
            string sql = $"INSERT INTO `route` (`Height`) VALUES ('{route.height}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public static int GetAutoIncrement()
        {
            int ai;
            string sql = "SELECT `AUTO_INCREMENT` FROM  INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'route'";

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            var row = dt.Rows[0];
            ai = Convert.ToInt32(row["AUTO_INCREMENT"]);

            return ai;
        }
    }
}
