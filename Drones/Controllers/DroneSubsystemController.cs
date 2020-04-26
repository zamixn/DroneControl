using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
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
            while(count-- > 0)
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

        public IActionResult sendLicensePlateInfo()
        {
            return View();
        }
        public IActionResult validateNumber()
        {
            return View();
        }
    }
}