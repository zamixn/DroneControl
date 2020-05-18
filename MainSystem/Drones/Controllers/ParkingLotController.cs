using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drones.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drones.Controllers
{
    public class ParkingLotController : Controller
    {
        public IActionResult Index()
        {
            return View("MainSystemIndexView");
        }
        public IActionResult showLotReservationList()
        {
            return View("ParkingLotReservationsListView");
        }
        public IActionResult createLot()
        {
            return View("ParkingLotFormView");
        }
        public IActionResult showLots()
        {
            List<ParkingLot> parkingLots = ParkingLot.SelectLots();
            return View("ParkingLotListView", parkingLots);
        }
        public IActionResult confirmInformation()
        {
            // create parking lot
            return View("/Views/DroneSubsystem/DroneRouteFormView.cshtml");
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