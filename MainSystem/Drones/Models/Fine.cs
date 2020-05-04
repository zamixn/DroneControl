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
        public int id { get; private set; }
        public DateTime date { get; private set; }
        public double sum { get; private set; }
        public FineState state { get; private set; }
        public int fk_reservation { get; private set; }


        public List<Fine> GetFines()
        {
            string sql = $"SELECT * FROM fine";
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
                    date = Convert.ToDateTime(row["Date"]),
                    sum = Convert.ToDouble(row["Sum"]),
                    state = (FineState)Convert.ToInt32(row["State"]),
                    fk_reservation = Convert.ToInt32(row["fk_reservation"])

                };

                list.Add(fine);
            }
            return list;
        }
        public static bool UpdateFine(Fine fine)
        {
            string sql = $"UPDATE `fine` SET `Date` = '{fine.date}', `Sum` = '{fine.sum}', `State` = '{fine.state}', `fk_reservation` = '{fine.fk_reservation}' WHERE `fine`.`id` = {fine.id}";
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
            string sql = $"INSERT INTO `fine` (`Date`, `Sum`, `State`, `fk_reservation`) VALUES ('{fine.date}', '{fine.sum}', '{fine.state}', '{fine.fk_reservation}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
