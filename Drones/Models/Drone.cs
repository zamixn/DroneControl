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
        public int id { get; private set; }
        public DroneState state { get; private set; }
        public double batteryRemaining { get; private set; }

        public static bool Create(Drone drone)
        {
            string sql = $"INSERT INTO `drone` (`BatteryRemaining`, `State`) VALUES ('{drone.batteryRemaining}', '{drone.state}')";
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
