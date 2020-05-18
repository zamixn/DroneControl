using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace Drones.Models
{
    public class Drone 
    {
        public int id { get; private set; }
        public string model { get; private set; }
        public DroneState state { get; private set; }
        public double batteryRemaining { get; private set; }

        public Drone()
        {
        }

        public Drone(string model, DroneState state, double batteryRemaining)
        {
            this.id = id;
            this.model = model;
            this.state = state;
            this.batteryRemaining = batteryRemaining;
        }
        public static Drone Select(int id)
        {
            string sql = $"SELECT * FROM drone WHERE id = {id}";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();
            if (dt.Rows.Count == 0)
                return null;
            var row = dt.Rows[0];

            Drone drone = new Drone
            {
                id = Convert.ToInt32(row["id"]),
                model = Convert.ToString(row["model"]),
                state = (DroneState)(Convert.ToInt32(row["State"]) - 1),
                batteryRemaining = Convert.ToDouble(row["BatteryRemaining"])
            };

            return drone;
        }

        public static bool UpdateState(Drone drone, int state)
        {
            string sql = $"UPDATE drone SET drone.State = '{state}' WHERE drone.id = '{drone.id}'";

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public static bool Create(Drone drone)
        {
            string sql = $"INSERT INTO `drone` (`model`, `BatteryRemaining`, `State`) VALUES ('{drone.model}', '{drone.batteryRemaining}', '{((int)drone.state + 1)}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }


        public static List<Drone> getDroneList()
        {
            string sql = $"SELECT * FROM `drone`";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            Debug.WriteLine(conn);
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<Drone> list = new List<Drone>();
            foreach (DataRow row in dt.Rows)
            {
                Drone drone = new Drone
                {
                    id = Convert.ToInt32(row["id"]),
                    model = Convert.ToString(row["model"]),
                    state = (DroneState)(Convert.ToInt32(row["State"]) - 1),
                    batteryRemaining = Convert.ToDouble(row["BatteryRemaining"])
                };

                list.Add(drone);
            }
            return list;
        }

        public static int getDroneIdFromName(string droneName)
        {
            int id = 0;
            string sql = $"SELECT drone.id FROM `drone` WHERE drone.model = '{droneName}'";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            Debug.WriteLine(conn);
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();
            foreach (DataRow row in dt.Rows)
            {
                id = Convert.ToInt32(row["id"]);
            }
            return id;
        }

        public static List<string> getDroneListString()
        {
            string sql = $"SELECT * FROM `drone`";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            Debug.WriteLine(conn);
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Convert.ToString(row["model"]));
            }
            return list;
        }

        public static Drone getReadyDrone()
        {
            string sql = $"SELECT * FROM `drone` WHERE BatteryRemaining = 100 AND State = 2";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            Debug.WriteLine(conn);
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<Drone> list = new List<Drone>();
            foreach (DataRow row in dt.Rows)
            {
                Drone drone = new Drone
                {
                    id = Convert.ToInt32(row["id"]),
                    model = Convert.ToString(row["model"]),
                    state = (DroneState)(Convert.ToInt32(row["State"]) - 1),
                    batteryRemaining = Convert.ToDouble(row["BatteryRemaining"])
                };

                list.Add(drone);
            }
            return list[0];
        }

        public static bool Delete(int id)
        {
            string sqlquery = $"DELETE FROM `drone` WHERE `drone`.`id` = {id}";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
    }
}
