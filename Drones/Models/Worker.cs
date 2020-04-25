using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drones.Models
{
    public class Worker
    {
        private int id;
        private string username;
        private string passwordHash;
        private int role; // 0 - admin, - 1 peon
        private string name;
        private string surname;

        public Worker Select()
        { throw new NotImplementedException(); }

        public List<Worker> GetWorkers()
        { throw new NotImplementedException(); }

        public int GetRole()
        { throw new NotImplementedException(); }

        public void UpdateWorker(Worker worker)
        { throw new NotImplementedException(); }
    }
}
