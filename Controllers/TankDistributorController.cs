using GasStation.Models;
using GasStation.Models.Contexts;
using GasStation.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Controllers
{
    public class TankDistributorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TankDistributorService _tankDistributorService;
        private readonly TankService _tankService;
        private readonly DistributorService _distributorService;
        public TankDistributorController(AppDbContext context, TankDistributorService tankDistributorService, TankService tankService, DistributorService distributorService)
        {
            _context = context;
            _tankDistributorService = tankDistributorService;
            _tankService = tankService;
            _distributorService = distributorService;
        }

        public IActionResult Index()
        {
            return View(_tankDistributorService.GetAllTankDistributors());
        }

        // GET: TankDistributor/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TankDistributor tankDistributor)
        {
            if (ModelState.IsValid)
            {
                _tankDistributorService.Create(tankDistributor.TankId, tankDistributor.DistributorId);
                TempData["Info"] = "Połączenie Zbiornik-Dystrybutor zostało dodane";
                return RedirectToAction(nameof(Index));
            }
            return View(tankDistributor);
        }

        // GET: TankDistributor/Edit/5
        public IActionResult Edit(int tankId, int distributorId)
        {


            var tank = _tankDistributorService.GetById(tankId, distributorId);
            if (tank == null)
            {
                return NotFound();
            }
            TankDistributor model = new TankDistributor
            {
                TankId = tankId,
                DistributorId = distributorId,
                Tank = _tankService.GetById(tankId),
                Distributor = _distributorService.GetById(distributorId)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int tankId, int distributorId, TankDistributor tankDistributor)
        {
            if (tankId != tankDistributor.TankId || distributorId != tankDistributor.DistributorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tankDistributorService.Edit(tankDistributor);
                    TempData["Info"] = "Dane połącznenie Zbiornika z Dystrybutorem zostało zaktualizowane";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_tankDistributorService.TankDistributorExists(tankId, distributorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Warning"] = "Wystąpił błąd podczas edycji połączenia Zbiorni-Dystrybutor";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tankDistributor);
        }

        public IActionResult Delete(int tankId, int distributorId)
        {

            try
            {
                _tankDistributorService.Delete(tankId, distributorId);
                TempData["Info"] = "Połączenie Zbiornik-Dystrybutor został usunięty";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania połączenie Zbiornik-Dystrybutor.";
                throw;
            }

            return RedirectToAction("Index", "Tank");
        }

        // POST: TankDistributor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tankDistributor = await _context.TankDistributors.FindAsync(id);
            _context.TankDistributors.Remove(tankDistributor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
