using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Drones.Models
{
    public class Coordinate
    {
        private string Longitude;
        private string Latitude;

        public static bool Create(Coordinate coordinate)
        {
            string sql = $"INSERT INTO `coordinate` (`Longitude`, `Latitude`) VALUES ('{coordinate.Longitude}', '{coordinate.Latitude}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
    }
}
