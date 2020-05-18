using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Drones.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UserSubsytemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotController : ControllerBase
    {

        // url: https://localhost:44355/api/parkingLot

        // GET api/parkingLot
        [HttpGet]
        public ActionResult<string> Get()
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

            List<ParkingLot> lots = new List<ParkingLot>();
            foreach (DataRow row in dt.Rows)
            {
                ParkingLot parkingLot = new ParkingLot
                    (
                    //id: row["id"] != System.DBNull.Value ? Convert.ToInt32(row["id"]) : -1,
                    address: row["Address"] != System.DBNull.Value ? Convert.ToString(row["Address"]) : "",
                    totalSpaces: row["TotalSpaces"] != System.DBNull.Value ? Convert.ToInt32(row["TotalSpaces"]) : -1,
                    reservedSpaces: row["ReservedSpaces"] != System.DBNull.Value ? Convert.ToInt32(row["ReservedSpaces"]) : -1,
                    state: row["State"] != System.DBNull.Value ? (ParkingLotState)Convert.ToInt32(row["State"]) : ParkingLotState.Closed,
                    fk_Drone: row["fk_Drone"] != System.DBNull.Value ? Convert.ToInt32(row["fk_Drone"]) : -1,
                    fk_RouteFrom: row["fk_RouteFrom"] != System.DBNull.Value ? Convert.ToInt32(row["fk_RouteFrom"]) : -1,
                    fk_RouteTo: row["fk_RouteTo"] != System.DBNull.Value ? Convert.ToInt32(row["fk_RouteTo"]) : -1,
                    lotCheckTimeSpan: row["NumberCheckTimeSpan"] != System.DBNull.Value ? Convert.ToInt32(row["NumberCheckTimeSpan"]) : -1,
                    lastDroneVisit: row["lastDroneVisit"] != System.DBNull.Value ? Convert.ToDateTime(row["lastDroneVisit"]) : new DateTime(1, 1, 1, 0, 0, 0)
                    );

                lots.Add(parkingLot);
            }

            return JsonConvert.SerializeObject(lots);
        }

        // GET api/parkingLot/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/parkingLot
        [HttpPost]
        public string Post([FromBody] string content)
        {
            var jsonObject = JsonConvert.DeserializeObject<JToken>(content);
            int hours = jsonObject.SelectToken("hours").ToObject<int>();
            int minutes = jsonObject.SelectToken("minutes").ToObject<int>();
            int parkingLot = jsonObject.SelectToken("parkingLot").ToObject<int>();
            string licensePlate = jsonObject.SelectToken("licensePlate").ToObject<string>();
            string phoneNumber = jsonObject.SelectToken("phoneNumber").ToObject<string>();

            Reservation reservation = new Reservation(licensePlate, phoneNumber, DateTime.UtcNow, new TimeSpan(hours, minutes, 0), parkingLot);
            Reservation.Insert(reservation);

            return string.Format("hours: {0}, minutes:{1}, lot:{2}, plate:{3}, number:{4}", hours, minutes, parkingLot, licensePlate, phoneNumber);
        }

        // PUT api/parkingLot/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/parkingLot/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
