using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Drones.Models
{
    public class Reservation
    {
        public string licensePlateNumber { get; private set; }
        public string ownerPhoneNumber { get; private set; }
        public DateTime reservationDate { get; private set; }
        public TimeSpan reservationDuration { get; private set; }
        public int fk_parkingLot { get; private set; }

        public void Insert(Reservation reservation)
        {
            string sql = $"INSERT INTO `reservation` (`LicensePlate`, `OwnerPhoneNumbers`, `ReservationDate`, `ReservationDuration`, `fk_parkingLot`) VALUES ('{reservation.licensePlateNumber}'," +
                $" '{reservation.ownerPhoneNumber}', '{reservation.reservationDate}', '{reservation.reservationDuration}', '{reservation.fk_parkingLot}')";

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public Reservation Select(string licensePlateNumber)
        {
            string sql = $"SELECT * FROM reservation WHERE LicensePlate = {licensePlateNumber}";
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

            Reservation reservation = new Reservation
            {
                licensePlateNumber = Convert.ToString(row["LicensePlate"]),
                ownerPhoneNumber = Convert.ToString(row["OwnerPhoneNumbers"]),
                reservationDate = Convert.ToDateTime(row["ReservationDate"]),
                reservationDuration = (TimeSpan)row["ReservationDuration"],
                fk_parkingLot = Convert.ToInt32(row["fk_parkingLot"])
            };

            return reservation;
        }
    }
}
