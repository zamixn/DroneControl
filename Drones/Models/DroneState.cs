using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drones.Models
{
    public enum DroneState
    {
        Off,
        Charging,
        OnTheWayToLot,
        Emergency,
        EmergencyLanding,
        OnTheWayToBase,
        PlateScan_start,
        PlateScan_scanning,
        PlateScan_end,
    }
}
