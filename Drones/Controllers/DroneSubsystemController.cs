using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View();
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
            return View("DroneListView");
        }
        public IActionResult checkInput()
        {
            return View();
        }
        public IActionResult openPenaltyForm()
        {
            return View("PenaltyFormView");
        }
        public IActionResult submitPenaltyData()
        {
            return View();
        }
        public IActionResult removeDrone()
        {
            return View();
        }
    }
}