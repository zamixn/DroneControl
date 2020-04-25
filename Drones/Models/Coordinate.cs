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
        public string Longitude { get; private set; }
        public string Latitude { get; private set; }
        public int fk_route { get; private set; }

        public static bool Create(Coordinate coordinate)
        {
            string sql = $"INSERT INTO `coordinate` (`longitude`, `latitude`, `fk_route`) VALUES ('{coordinate.Longitude}', '{coordinate.Latitude}', '{coordinate.fk_route}')";
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
