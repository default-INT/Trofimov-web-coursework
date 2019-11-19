using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepairServiceCenterASP.Data;

namespace RepairServiceCenterASP.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly RepairServiceCenterContext conetex;

        public EmployeesController(RepairServiceCenterContext conetex)
        {
            this.conetex = conetex;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}