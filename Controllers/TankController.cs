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
    public class TankController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TankService _tankService;
        private readonly ProductService _productService;
        public TankController(AppDbContext context, TankService tankService, ProductService productService)
        {
            _context = context;
            _tankService = tankService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_tankService.GetAllTanks());
        }

        // GET: Tank/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tank = await _context.Tanks
                .FirstOrDefaultAsync(m => m.TankId == id);
            if (tank == null)
            {
                return NotFound();
            }

            return View(tank);
        }

        // GET: Tank/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tank tank)
        {
            if (ModelState.IsValid)
            {
                Tank modelToAdd = new Tank
                {
                    Capacity = tank.Capacity,
                    Stock = tank.Stock,
                    ProductId = tank.ProductId,
                    Product = _productService.GetById(tank.ProductId)
                };
                _tankService.Create(tank);
                TempData["Info"] = "Zbiornik został dodany";
                return RedirectToAction(nameof(Index));
            }
            return View(tank);
        }

        // GET: Tank/Edit/5
        public IActionResult Edit(int id)
        {


            var tank = _tankService.GetById(id);
            if (tank == null)
            {
                return NotFound();
            }
            Tank model = new Tank
            {
                TankId = tank.TankId,
                ProductId = tank.ProductId,
                Capacity = tank.Capacity,
                Stock = tank.Stock,
                Product = tank.Product
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Tank tank)
        {
            if (id != tank.TankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tankService.Edit(tank);
                    TempData["Info"] = "Dane zbiorniki zostały zaktualizowane";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_tankService.TankExists(tank.TankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Warning"] = "Wystąpił błąd podczas edycji zbiornika";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tank);
        }

        public IActionResult Delete(int id)
        {

            try
            {
                _tankService.Delete(id);
                TempData["Info"] = "Zbiornik został usunięty";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania zbiornika.";
                throw;
            }

            return RedirectToAction("Index", "Tank");
        }

        // POST: Tank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tank = await _context.Tanks.FindAsync(id);
            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
