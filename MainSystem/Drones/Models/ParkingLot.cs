using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace Drones.Models
{
    public class ParkingLot
    {
        public int id { get; private set; }
        public string address { get; private set; }
        public int totalSpaces { get; private set; }
        public int reservedSpaces { get; private set; }
        public ParkingLotState state { get; private set; }
        public int lotCheckTimeSpan { get; private set; }
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime lastDroneVisit { get; private set; }
        public int fk_Drone { get; private set; }
        public int fk_RouteFrom { get; private set; }
        public int fk_RouteTo { get; private set; }

        public ParkingLot(string address, int totalSpaces, int reservedSpaces, ParkingLotState state, int lotCheckTimeSpan, DateTime lastDroneVisit,int fk_Drone, int fk_RouteFrom, int fk_RouteTo)
        {
            this.id = id;
            this.address = address;
            this.totalSpaces = totalSpaces;
            this.reservedSpaces = reservedSpaces;
            this.state = state;
            this.lotCheckTimeSpan = lotCheckTimeSpan;
            this.lastDroneVisit = lastDroneVisit;
            this.fk_Drone = fk_Drone;
            this.fk_RouteFrom = fk_RouteFrom;
            this.fk_RouteTo = fk_RouteTo;
        }

        public ParkingLot()
        {
        }

        public static void Create(ParkingLot parkingLot)
        {
            string sql = $"INSERT INTO `parkinglot` (`Address`, `TotalSpaces`, `ReservedSpaces`, `State`, `fk_Drone`, `fk_RouteFrom`, `fk_RouteTo`, `numberCheckTimeSpan`, `lastDroneVisit`) VALUES ('{parkingLot.address}', '{parkingLot.totalSpaces}', '{parkingLot.reservedSpaces}', " +
                $"'{(int)parkingLot.state}', '{parkingLot.fk_Drone}', '{parkingLot.fk_RouteFrom}', '{parkingLot.fk_RouteTo}', '{parkingLot.lotCheckTimeSpan}', '{parkingLot.lastDroneVisit}')";
            Debug.WriteLine(sql);
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public static List<ParkingLot> SelectLots()
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
                    lotCheckTimeSpan = Convert.ToInt32(row["numberCheckTimeSpan"]),
                    lastDroneVisit = DBNull.Value != row["lastDroneVisit"] ? Convert.ToDateTime(row["lastDroneVisit"]) : DateTime.MinValue,
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

        public static void UpdateRouteFrom(int fk_route)
        {
            string sqlquery = $"UPDATE  `parkinglot` set `fk_RouteFrom`= {fk_route} WHERE id = (select max(id) from `parkinglot`)";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public static void UpdateRouteTo(int fk_route)
        {
            string sqlquery = $"UPDATE  `parkinglot` set `fk_RouteTo`= {fk_route} WHERE id = (select max(id) from `parkinglot`)";
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
