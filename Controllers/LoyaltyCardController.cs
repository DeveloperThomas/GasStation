using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GasStation.Models;
using GasStation.Models.Contexts;
using GasStation.Service;
using GasStation.Models.ModelsView;
using System.Threading.Tasks;

namespace GasStation.Controllers
{
    public class LoyaltyCardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LoyaltyCardService _loyaltyCardService;
        public LoyaltyCardController(AppDbContext context, LoyaltyCardService loyaltyCardService)
        {

            _context = context;
            _loyaltyCardService = loyaltyCardService;
        }

        public IActionResult Index()
        {
            return View(_loyaltyCardService.GetAllLoyaltyCards());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loyaltyCard = await _context.LoyaltyCards
                .FirstOrDefaultAsync(m => m.LoyaltyCardId == id);
            if (loyaltyCard == null)
            {
                return NotFound();
            }

            return View(loyaltyCard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoyaltyCard loyaltyCard)
        {
            if (ModelState.IsValid)
            {
                loyaltyCard.IssueDate = DateTime.Now;
                _loyaltyCardService.Create(loyaltyCard);
                TempData["Info"] = "Karta została dodana";
                return RedirectToAction(nameof(Index));
            }
            return View(loyaltyCard);
        }

        public IActionResult Edit(int id)
        {


            var loyaltyCard = _loyaltyCardService.GetById(id);
            if (loyaltyCard == null)
            {
                return NotFound();
            }
            return View(loyaltyCard);
        }

        [HttpPost]
        public IActionResult Edit(int id, LoyaltyCard loyaltyCard)
        {
            if (id != loyaltyCard.LoyaltyCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _loyaltyCardService.Edit(loyaltyCard);
                    TempData["Info"] = "Dane karty zostały zaktualizowane";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_loyaltyCardService.LoyaltyCardExists(loyaltyCard.LoyaltyCardId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Warning"] = "Wystąpił błąd podczas edycji karty";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loyaltyCard);
        }

        public IActionResult Delete(int id)
        {

            try
            {
                _loyaltyCardService.Delete(id);
                TempData["Info"] = "Karta została usunięta";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania karty.";
                throw;
            }

            return RedirectToAction("Index", "LoyaltyCard");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id);
            _context.LoyaltyCards.Remove(loyaltyCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}