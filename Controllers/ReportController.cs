using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Models;
using GasStation.Service;

using Microsoft.AspNetCore.Mvc;

namespace GasStation.Controllers
{
    public class ReportController : Controller
    {
        private readonly ProductService _productService;

        public ReportController(ProductService productService)
        {
            _productService = productService;
        }

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

            //var products = _productService.GetFuelingBeetwenDates(fuelingReport.DateFrom, fuelingReport.DateTo);
            //fuelingReport.Fuelings = new List<Fueling>();
            //if (products != null)
            //{
            //    fuelingReport.Fuelings = products;
            //}


            //return View("ReportOfFueling", fuelingReport);
        }

    }
}
