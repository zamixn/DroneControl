using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Drones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drones.Controllers
{
    public class DroneSubsystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult createDroneFromRoute()
        {
            return View("DroneRouteFormFromView");
        }
        public IActionResult createDroneToRoute()
        {
            return View("DroneRouteFormToView");
        }
        public IActionResult openDroneFormView()
        {
            return View("DroneFormView");
        }
        public IActionResult submitDroneData()
        {
            return View();
        }
        public IActionResult validateDroneData()
        {
            return View();
        }
        public IActionResult openDroneListView()
        {
            List<Drone> drones = Drone.getDroneList(); // <- this is a list, for erick
            return View("DroneListView", drones);
        }
        // drone route creation
        [HttpPost]
        public IActionResult checkInputFrom()
        {
            double height = double.Parse(Request.Form["height"]);
            int fk_route = Route.GetAutoIncrement();
            Route.Create(new Route(height));
            List<Coordinate> coords = new List<Coordinate>();
            int index = 0;

            int count = 10000;
            while (count-- > 0)
            {
                string lat = Request.Form["lat_" + index];
                string lon = Request.Form["lon_" + index];
                index++;
                if (lat == null || lon == null || lat == " " || lon == " ")
                    continue;
                Coordinate.Create(new Coordinate(lon, lat, fk_route));
            }
            ParkingLot.UpdateRouteFrom(fk_route);
            return View("DroneRouteFormToView");
        }
        [HttpPost]
        public IActionResult checkInputTo()
        {
            double height = double.Parse(Request.Form["height"]);
            int fk_route = Route.GetAutoIncrement();
            Route.Create(new Route(height));
            List<Coordinate> coords = new List<Coordinate>();
            int index = 0;

            int count = 10000;
            while (count-- > 0)
            {
                string lat = Request.Form["lat_" + index];
                string lon = Request.Form["lon_" + index];
                index++;
                if (lat == null || lon == null || lat == " " || lon == " ")
                    continue;
                Coordinate.Create(new Coordinate(lon, lat, fk_route));
            }
            ParkingLot.UpdateRouteTo(fk_route);
            List<ParkingLot> parkingLots = ParkingLot.SelectLots();
            return View("/Views/ParkingLot/ParkingLotListView.cshtml", parkingLots);
        }
        public IActionResult removeDrone(int id)
        {
            Drone.Delete(id);
            return openDroneListView();
        }

        [HttpPost]
        public IActionResult createDrone()
        {
            string _model = Request.Form["model"];
            Drone.Create(new Drone(_model, DroneState.Off, 100));
            return openDroneListView();
        }

        public static List<string> plates = new List<string>();
        public static void receiveLicensePlateInfo()
        {

        }
        public IActionResult tempDroneForm()
        {
            return View("tempDroneForm", plates);
        }

        public static void validateNumber()
        {
            string plate = "abc123";
            Reservation reservation = Reservation.Select(plate);

            if(reservation == null)
            {
                Debug.WriteLine("=========================================================");
                Debug.WriteLine("Reservation null");
                Debug.WriteLine("Take loss");
                Debug.WriteLine("=========================================================");
            }
            else
            {
                //Used for testing. Format(Year, Month, Day, Hour, Minute, Second)
                //DateTime end = new DateTime(2020, 5, 22, 23, 50, 0);

                DateTime start = reservation.reservationDate;
                TimeSpan duration = reservation.reservationDuration;
                DateTime end = start.Add(duration);

                int result = DateTime.Compare(DateTime.Now, end);
                if (result > 0)
                {
                    TimeSpan time = DateTime.Now.Subtract(end);
                    Fine fine = Fine.SelectByReservation(reservation.id);

                    //Debug.WriteLine("=========================================================");
                    //Debug.WriteLine("Time spent");
                    //Debug.WriteLine(time.TotalMinutes);
                    //Debug.WriteLine("=========================================================");
                    if (fine == null)
                    {
                        fine = new Fine(DateTime.Now, GetFineAmmount(time), FineState.Formed, reservation.id);
                        Fine.Create(fine);
                    }
                    else
                    {
                        Fine.UpdateFineSum(fine, GetFineAmmount(time));
                        //Debug.WriteLine("=========================================================");
                        //Debug.WriteLine("Minutes");
                        //Debug.WriteLine(duration.TotalMinutes);
                        //Debug.WriteLine("=========================================================");
                    }
                    //Debug.WriteLine("=========================================================");
                    //Debug.WriteLine("You gonna get fined boy");
                    //Debug.WriteLine("{0} {1}", DateTime.Now, end);
                    //Debug.WriteLine("=========================================================");
                }
            }
        }
        public static double GetFineAmmount(TimeSpan duration)
        {
            double fine = duration.TotalMinutes * 0.2;
            //Debug.WriteLine("=========================================================");
            //Debug.WriteLine("Fine ammount");
            //Debug.WriteLine(fine);
            //Debug.WriteLine("=========================================================");
            return fine;
        }
        public static void parkingLotDroneSender()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(test);
            aTimer.Interval = 10000; //PASITIKRINIMUI 10000. INTERVALA NUSTATYTI KAS 1 MIN - 60000
            aTimer.Enabled = true;
        }
        public static void test(object source, ElapsedEventArgs e)
        {
            List<ParkingLot> lots = ParkingLot.SelectLots();
            foreach (ParkingLot lot in lots)
            {
                if (DateTime.Now - lot.lastDroneVisit > TimeSpan.FromMinutes(lot.lotCheckTimeSpan))
                {
                    Drone drone = Drone.Select(lot.fk_Drone);
                    sendDrone(lot, drone);
                    Debug.WriteLine("Siusti drona i {0} aikstele", lot.address);
                    Debug.WriteLine("=========================================================");               
                }
            }
        }
        public static void sendDrone(ParkingLot lot, Drone drone)
        {
            if (drone.state == DroneState.Charging)
            {
                Drone.UpdateState(drone, (int)DroneState.OnTheWayToLot);
                ParkingLot.UpdateDroneVisitTime(lot.id);
            }
        }
        public IActionResult delete()
        {
            List<Drone> drones = Drone.getDroneList();
            return View("DroneListView", drones);
        }
    }
}