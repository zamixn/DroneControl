using System;
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
        public IActionResult createDroneRoute()
        {
            //dynamic mymodel = new ExpandoObject();
            //mymodel.coordinates = GetStudents();
            return View("DroneRouteFormView");
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
        public IActionResult checkInput()
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
            return View("/Views/ParkingLot/ParkingLotListView.cshtml");
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
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    string py = "/c C:\\Python27\\python.exe server.py";
                    ProcessStartInfo info = new ProcessStartInfo("cmd", py);
                    info.CreateNoWindow = false;
                    info.UseShellExecute = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    var p = Process.Start(info);
                    p.WaitForExit();

                    string plateFile = "plate.txt";
                    string plate = System.IO.File.ReadAllText(plateFile);
                    plates.Add(plate);
                    Debug.WriteLine("plate is: " + plate);

                }
            });
            t.Start();
        }
        public IActionResult tempDroneForm()
        {
            return View("tempDroneForm", plates);
        }

        public IActionResult validateNumber()
        {
            return View();
        }
    }
}