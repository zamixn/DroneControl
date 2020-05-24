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
using System.Net;

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

        public static void ValidateNumber(string plate)
        {
            Reservation reservation = Reservation.Select(plate);
            Debug.WriteLine("ValidateNumber");

            if (reservation == null)
            {
                Debug.WriteLine("=========================================================");
                Debug.WriteLine("Reservation null");
                Debug.WriteLine("Take loss");
                Debug.WriteLine("=========================================================");
            }
            else
            {
                Debug.WriteLine("=========================================================");
                Debug.WriteLine("Reservation not null");

                //Used for testing. Format(Year, Month, Day, Hour, Minute, Second)
                //DateTime end = new DateTime(2020, 5, 22, 23, 50, 0);
                CreateFine(reservation);
            }
        }
        public static void CreateFine(Reservation reservation)
        {

            DateTime start = reservation.reservationDate;
            TimeSpan duration = reservation.reservationDuration;
            DateTime end = start.Add(duration);

            int result = DateTime.Compare(DateTime.Now, end);
            if (result > 0)
            {
                Debug.WriteLine("=========================================================");
                Debug.WriteLine("You gonna get fined boy");
                Debug.WriteLine("{0} {1}", DateTime.Now, end);
                Debug.WriteLine("=========================================================");

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
            SendSignalToDrone(null, null);
            aTimer.Elapsed += new ElapsedEventHandler(SendSignalToDrone);
            aTimer.Interval = 60000; // kas minute
            aTimer.Enabled = true;
        }
        public static void SendSignalToDrone(object source, ElapsedEventArgs e)
        {
            Debug.WriteLine("SendSignalToDrone");
            Debug.WriteLine("=========================================================");
            List<ParkingLot> lots = ParkingLot.SelectLots();
            foreach (ParkingLot lot in lots)
            {
                if (lot.state == ParkingLotState.Open)
                {
                    if (DateTime.Now - lot.lastDroneVisit > TimeSpan.FromMinutes(lot.lotCheckTimeSpan))
                    {
                        Drone drone = Drone.Select(lot.fk_Drone);

                        if (drone.state == DroneState.Charging)
                        {
                            Debug.WriteLine("Siusti drona i {0} aikstele", lot.address);
                            Debug.WriteLine("=========================================================");

                            string data = "skrisk"; // realiai butu marsrutas i aikstele
                            SendSignal(data);

                            Drone.UpdateState(drone, (int)DroneState.OnTheWayToLot);
                            ParkingLot.UpdateDroneVisitTime(lot.id, DateTime.Now);

                            // TESTAVIMUI: realiai dronas taps charhing, kai sugris i baze
                            Drone.UpdateState(drone, (int)DroneState.Charging);
                            // TESTAVIMUI: realiai 
                            ParkingLot.UpdateDroneVisitTime(lot.id, DateTime.Now.AddYears(-200));

                            Debug.WriteLine("=========================================================");
                        }
                    }
                }
            }
        }
        public static void SendSignal(string data)
        {
            string ip = "192.168.1.217";
            int port = 8080;

            Debug.WriteLine("Sending signal to drone");

            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                client.Connect(remoteEP);

                data += "<EOF>";
                // Send test data to the remote device.
                client.Send(Encoding.UTF8.GetBytes(data));

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public IActionResult delete()
        {
            List<Drone> drones = Drone.getDroneList();
            return View("DroneListView", drones);
        }
    }
}