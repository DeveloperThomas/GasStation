using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Models;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult ReportOfSales()
        {
            var model = new ReportSale();
            model.DateFrom = DateTime.Today;
            model.DateTo = DateTime.Today;
            model.Products = new List<Product>();
            return View(model);
        }
        [HttpPost]
        public IActionResult ReportOfSales(ReportSale model)
        {
            return View();
        }

    }
}
