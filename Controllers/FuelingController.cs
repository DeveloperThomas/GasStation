using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Models;
using GasStation.Models.ModelsView;
using GasStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.Controllers
{
    public class FuelingController : Controller
    {
        FuelingService _fuelingService;
        public FuelingController(FuelingService fuelingService)
        {
            _fuelingService = fuelingService;
        }

        public IActionResult ReportOfFueling()
        {
            var model = new FuelingReport();
            model.DateFrom = DateTime.Today;
            model.DateTo = DateTime.Today;
            model.Fuelings = new List<Fueling>();
            return View(model);
        }
        [HttpPost]
        public IActionResult ReportOfFueling(FuelingReport fuelingReport)
        {
            return RedirectToAction("GenerateFuelings", fuelingReport);
        }
        public IActionResult GenerateFuelings(FuelingReport fuelingReport)
        {
            var fuelings = _fuelingService.GetFuelingBeetwenDates(fuelingReport.DateFrom, fuelingReport.DateTo);
            fuelingReport.Fuelings = new List<Fueling>();
            if(fuelings!=null)
            {
                 fuelingReport.Fuelings = fuelings;
            }
        

            return View("ReportOfFueling", fuelingReport);
        }
    }
}
