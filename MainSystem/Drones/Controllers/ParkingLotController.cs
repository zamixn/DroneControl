﻿using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Drones.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        [HttpPost]
        public IActionResult confirmInformation()
        {
            string adress = Request.Form["address"];
            int totalSpaces = int.Parse(Request.Form["totalSpaces"]);
            int reservedSpaces = int.Parse(Request.Form["reservedSpaces"]);
            string stateString = Request.Form["state"];
            ParkingLotState state = (ParkingLotState)Enum.Parse(typeof(ParkingLotState), stateString, true);
            string selectedDrone = Request.Form["fk_drone"];
            int fk_drone = Drone.getDroneIdFromName(selectedDrone);
            int checkInterval = int.Parse(Request.Form["checkTimeSpan"]);
            DateTime lastVisit = DateTime.Parse(Request.Form["lastDroneVisit"]);
            ParkingLot.Create(new ParkingLot(adress, totalSpaces, reservedSpaces, state, checkInterval, lastVisit, fk_drone, -1, -1));

            //// create parking lot
            return View("/Views/DroneSubsystem/DroneRouteFormFromView.cshtml");
        }
        public IActionResult showReservations()
        {
            return View("ParkingLotReservationsListView");
        }
        public IActionResult selectDelete()
        {
            return View();
        }
        public IActionResult delete()
        {
            List<ParkingLot> parkingLots = ParkingLot.SelectLots();
            return View("ParkingLotListView", parkingLots);
        }
    }
}