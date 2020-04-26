using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Drones.Models
{
    public class ParkingLot
    {
        public int id { get; private set; }
        public string address { get; private set; }
        public int totalSpaces { get; private set; }
        public int reservedSpaces { get; private set; }
        public ParkingLotState state { get; private set; }
        public int fk_Drone { get; private set; }
        public int fk_RouteFrom { get; private set; }
        public int fk_RouteTo { get; private set; }


        public void Create(ParkingLot parkingLot)
        {
            string sql = $"INSERT INTO `parkinglot` (`Address`, `TotalSpaces`, `ReservedSpaces`, `State`, `fk_Drone`, `fk_RouteFrom`, `fk_RouteTo`) VALUES ('{parkingLot.address}', '{parkingLot.totalSpaces}', '{parkingLot.reservedSpaces}', " +
                $"'{parkingLot.state}', '{parkingLot.fk_Drone}', '{parkingLot.fk_RouteFrom}', '{parkingLot.fk_RouteTo}')";

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public List<ParkingLot> SelectLots()
        {
            string sql = $"SELECT * FROM parkinglot";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<ParkingLot> list = new List<ParkingLot>();
            foreach (DataRow row in dt.Rows)
            {
                ParkingLot parkingLot = new ParkingLot
                {
                    id = Convert.ToInt32(row["id"]),
                    address = Convert.ToString(row["Address"]),
                    totalSpaces = Convert.ToInt32(row["TotalSpaces"]),
                    reservedSpaces = Convert.ToInt32(row["ReservedSpaces"]),
                    state = (ParkingLotState)Convert.ToInt32(row["State"]),
                    fk_Drone = Convert.ToInt32(row["fk_Drone"]),
                    fk_RouteFrom = Convert.ToInt32(row["fk_RouteFrom"]),
                    fk_RouteTo = Convert.ToInt32(row["fk_RouteTo"])

                };

                list.Add(parkingLot);
            }
            return list;
        }

        public static bool Delete(ParkingLot parkingLot)
        {
            string sqlquery = $"DELETE FROM `parkinglot` WHERE `parkinglot`.`id` = {parkingLot.id}";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public static List<Coordinate> GetParkingLotRouteId(ParkingLot parkingLot)
        {
            string sql = $"SELECT coordinate.longitude as longitude, coordinate.latitude as latitude, coordinate.fk_route as fk_route FROM `route` LEFT JOIN coordinate on route.id = coordinate.id WHERE coordinate.fk_route = {parkingLot.fk_RouteFrom};";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();
            mda.Dispose();

            List<Coordinate> list = new List<Coordinate>();
            foreach (DataRow row in dt.Rows)
            {
                Coordinate coordinate = new Coordinate(Convert.ToString(row["longitude"]), Convert.ToString(row["latitude"]), Convert.ToInt32(row["fk_route"]));
                list.Add(coordinate);
            }
            return list;
        }
    }
}
