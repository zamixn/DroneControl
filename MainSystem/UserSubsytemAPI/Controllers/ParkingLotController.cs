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
            List<ParkingLot> lots = ParkingLot.SelectLots();
            return JsonConvert.SerializeObject(lots);
        }

        // POST api/parkingLot
        [HttpPost]
        public string Post([FromBody] string content)
        {
            Debug.WriteLine("ReceivedPost: " + content);
            var jsonObject = JsonConvert.DeserializeObject<JToken>(content);
            int hours = jsonObject.SelectToken("hours").ToObject<int>();
            int minutes = jsonObject.SelectToken("minutes").ToObject<int>();
            int parkingLot = jsonObject.SelectToken("parkingLot").ToObject<int>();
            string licensePlate = jsonObject.SelectToken("licensePlate").ToObject<string>();
            string phoneNumber = jsonObject.SelectToken("phoneNumber").ToObject<string>();

            Reservation reservation = new Reservation(licensePlate, phoneNumber, DateTime.Now, new TimeSpan(hours, minutes, 0), parkingLot);
            Reservation.Insert(reservation);

            return string.Format("hours: {0}, minutes:{1}, lot:{2}, plate:{3}, number:{4}", hours, minutes, parkingLot, licensePlate, phoneNumber);
        }
    }
}
