using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GasStation.Models;
using GasStation.Models.Contexts;
using GasStation.Service;
using GasStation.Models.ModelsView;

namespace GasStation.Controllers
{
    public class DistributorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DistributorService _distributorService;
        private readonly ProductService _productService;
        private readonly TankService _tankService;
        public DistributorController(AppDbContext context, DistributorService distributorService, ProductService productService, TankService tankService)
        {
            _context = context;
            _distributorService = distributorService;
            _productService = productService;
            _tankService = tankService;
        }

        public IActionResult Index()
        {
            return View(_distributorService.GetAllDistributors());
        }

        // GET: Distributor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributor = await _context.Distributors
                .FirstOrDefaultAsync(m => m.DistributorId == id);
            if (distributor == null)
            {
                return NotFound();
            }

            return View(distributor);
        }

        // GET: Distributor/Create
        public IActionResult Create()
        {
            ViewData["TankIds"] = new SelectList(_tankService.GetAllTanks(), "TankId", "TankId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DistributorCreate distributorCreate)
        {
            if (ModelState.IsValid)
            {
                _distributorService.Create(distributorCreate);
                TempData["Info"] = "Dytrybutor został dodany";
                return RedirectToAction(nameof(Index));
            }
            return View(distributorCreate);
        }

        // GET: Distributor/Edit/5
        public IActionResult Edit(int id)
        {


            var distributor = _distributorService.GetById(id);
            if (distributor == null)
            {
                return NotFound();
            }
            Distributor model = new Distributor
            {
                DistributorId = distributor.DistributorId,
                IsLocked = distributor.IsLocked,
                Counter = distributor.Counter,
                Fuelings = distributor.Fuelings,
                TankDistributors = distributor.TankDistributors
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Distributor distributor)
        {
            if (id != distributor.DistributorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _distributorService.Edit(distributor);
                    TempData["Info"] = "Dane dytrybutory zostały zaktualizowane";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_distributorService.DistributorExists(distributor.DistributorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Warning"] = "Wystąpił błąd podczas edycji dystrybutora";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(distributor);
        }

        public IActionResult Delete(int id)
        {

            try
            {
                _distributorService.Delete(id);
                TempData["Info"] = "Dystrybutor został usunięty";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania dystrybutora.";
                throw;
            }

            return RedirectToAction("Index", "Distributor");
        }

        public IActionResult Preview(int id)
        {

            List<Tank> tanks = new List<Tank>();
            List<TankDistributor> tanksAssignToDistributor = _distributorService.GetTanksFroDistributor(id);


            foreach (var item in tanksAssignToDistributor)
            {
                Tank model = _tankService.GetById(item.TankId);
                tanks.Add(model);
            }

            return View(tanks);
        }
    }
}
