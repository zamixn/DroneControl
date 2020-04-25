using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Drones.Models
{
    public class Fine
    {
        private int id;
        private DateTime date;
        private double sum;
        private FineState state;

        public List<Fine> GetFines()
        {
            string sql = $"SELECT * FROM Fine";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<Fine> list = new List<Fine>();
            foreach (DataRow row in dt.Rows)
            {
                Fine fine = new Fine
                {
                    id = Convert.ToInt32(row["id"]),
                    date = Convert.ToDateTime(row["date"]),
                    sum = Convert.ToDouble(row["sum"]),
                    state = (FineState)Convert.ToInt32(row["state"])

                };

                list.Add(fine);
            }
            return list;
        }
        public static bool UpdateFine(Fine fine)
        {
            string sql = $"UPDATE `Fine` SET `date` = '{fine.date}', `sum` = '{fine.sum}', `state` = '{fine.state}' WHERE `Fine`.`id` = {fine.id}";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void Create(Fine fine)
        {
            string sql = $"INSERT INTO `Fine` (`date`, `sum`, `state`) VALUES ('{fine.date}', '{fine.sum}', '{fine.state}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
