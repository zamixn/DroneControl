using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Drones.Controllers
{
    public class ParkingLotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult showLotReservationList()
        {
            return View();
        }
        public IActionResult createLot()
        {
            return View();
        }
        public IActionResult showLots()
        {
            return View();
        }
        public IActionResult confirmInformation()
        {
            return View();
        }
        public IActionResult showReservations()
        {
            return View();
        }
        public IActionResult selectDelete()
        {
            return View();
        }
        public IActionResult delete()
        {
            return View();
        }
    }
}