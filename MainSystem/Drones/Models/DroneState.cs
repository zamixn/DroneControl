using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Drones.Models
{
    public enum DroneState
    {
        Off = 1,
        Charging = 2,
        OnTheWayToLot = 3,
        Emergency = 4,
        EmergencyLanding = 5,
        OnTheWayToBase = 6,
        PlateScan_start = 7,
        PlateScan_scanning = 8,
        PlateScan_end = 9,
    }
}
