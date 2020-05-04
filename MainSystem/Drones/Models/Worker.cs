using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Drones.Models
{
    public class Worker
    {
        public int id { get; private set; }
        public string username { get; private set; }
        public string passwordHash { get; private set; }
        public int role { get; private set; } // 0 - admin, - 1 peon
        public string name { get; private set; }
        public string surname { get; private set; }

        public Worker Select(int id)
        {
            string sql = $"SELECT * FROM worker WHERE id = {id}";
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

            Worker worker = new Worker
            {
                id = Convert.ToInt32(row["id"]),
                username = Convert.ToString(row["Username"]),
                passwordHash = Convert.ToString(row["PasswordHash"]),
                role = Convert.ToInt32(row["WorkerRole"]),
                name = Convert.ToString(row["Name"]),
                surname = Convert.ToString(row["Surname"])
            };

            return worker;
        }
        public List<Worker> GetWorkers()
        {
            string sql = $"SELECT * FROM worker";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<Worker> list = new List<Worker>();
            foreach (DataRow row in dt.Rows)
            {
                Worker worker = new Worker
                {
                    id = Convert.ToInt32(row["id"]),
                    username = Convert.ToString(row["Username"]),
                    passwordHash = Convert.ToString(row["PasswordHash"]),
                    role = Convert.ToInt32(row["WorkerRole"]),
                    name = Convert.ToString(row["Name"]),
                    surname = Convert.ToString(row["Surname"])

                };

                list.Add(worker);
            }
            return list;
        }

        public static bool ChangeRole(int id, int role)
        {
            string sql = $"UPDATE `worker` SET  `WorkerRole` = '{role}' WHERE `worker`.`id` = {id}";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public static bool UpdateWorker(Worker worker)
        {
            string sql = $"UPDATE `worker` SET `Username` = '{worker.username}', `PasswordHash` = '{worker.passwordHash}', `WorkerRole` = '{worker.role}'," +
                $" `Name` = '{worker.name}', `Surname` = '{worker.surname}' WHERE `worker`.`id` = {worker.id}";

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
