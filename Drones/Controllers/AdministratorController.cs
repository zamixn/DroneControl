using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Drones.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OpenWorkerView()
        {
            return View();
        }
        public IActionResult SelectRole()
        {
            return View();
        }
        public IActionResult ChooseRole()
        {
            return View();
        }
    }
}