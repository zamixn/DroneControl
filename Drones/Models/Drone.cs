using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Drones.Models
{
    public class Drone
    {
        private int id;
        private DroneState state;
        private double batteryRemaining;

        public static bool Create(Drone drone)
        {
            string sql = $"INSERT INTO `Drone` (`state`, `batteryRemaining`) VALUES ('{drone.state}', '{drone.batteryRemaining}')";
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
