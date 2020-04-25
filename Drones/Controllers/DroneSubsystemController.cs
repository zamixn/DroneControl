using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return View();
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
        public IActionResult checkInput()
        {
            return View();
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