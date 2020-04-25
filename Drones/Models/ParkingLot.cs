using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drones.Models
{
    public class ParkingLot
    {
        private int id;
        private string address;
        private int totalSpaces;
        private int reservedSpaces;
        private ParkingLotState state;   
        
        public ParkingLot Show()
        { throw new NotImplementedException(); }

        public void Crate(ParkingLot parkingLot)
        { throw new NotImplementedException(); }

        public List<ParkingLot> SelectLots()
        { throw new NotImplementedException(); }

        public void Delete(ParkingLot parkingLot)
        { throw new NotImplementedException(); }
    }
}
