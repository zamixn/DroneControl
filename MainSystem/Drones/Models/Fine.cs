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

        public Fine()
        {
        }
        public Fine(DateTime date, double sum, FineState state, int fk_reservation)
        {
            this.id = id;
            this.date = date;
            this.sum = sum;
            this.state = state;
            this.fk_reservation = fk_reservation;
        }

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
        public static Fine SelectByReservation(int reservationId)
        {
            string sql = $"SELECT * FROM fine WHERE fk_reservation = {reservationId}";
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

            Fine fine = new Fine
            {
                id = Convert.ToInt32(row["id"]),
                date = Convert.ToDateTime(row["Date"]),
                sum = Convert.ToDouble(row["Sum"]),
                state = (FineState)Convert.ToInt32(row["State"]),
                fk_reservation = Convert.ToInt32(row["fk_reservation"])
            };

            return fine;
        }
        public static bool UpdateFineSum(Fine fine, double sum)
        {
            string sql = $"UPDATE fine SET fine.Sum = '{sum}' WHERE fine.id = '{fine.id}'";

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public static void Create(Fine fine)
        {
            string sql = $"INSERT INTO `fine` (`Date`, `Sum`, `State`, `fk_reservation`) VALUES ('{fine.date.ToString("yyyy-MM-dd HH:mm:ss.fff")}', '{fine.sum}', '{(int)fine.state + 1}', '{fine.fk_reservation}')";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
