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
    public class DiscountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DiscountService _discountService;
        private readonly ProductService _productService;
        public DiscountController(AppDbContext context, DiscountService discountService, ProductService productService)
        {
            _context = context;
            _discountService = discountService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_discountService.GetAllDiscounts());
        }


        public IActionResult Create()
        {
            DiscountCreate model = new DiscountCreate
            {
                TypeOfDiscount = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "0", Text = "Wartościowa" },
                        new SelectListItem { Value = "1", Text = "Procentowa" },

                    }
            };
            ViewData["ProductId"] = new SelectList(_productService.GetAllProducts(), "ProductId", "Name");

            model.BeginDate = DateTime.UtcNow;
            model.FinishDate = DateTime.UtcNow;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DiscountCreate discountAdd)
        {
            if (ModelState.IsValid)
            {
                Discount modelToAdd = new Discount
                {
                    Name = discountAdd.Name,
                    Type = discountAdd.Type,
                    Value = discountAdd.Value,
                    BeginDate = discountAdd.BeginDate,
                    FinishDate = discountAdd.FinishDate,
                    ProductId=discountAdd.ProductId
                };
                _discountService.Create(modelToAdd);
                TempData["Info"] = "Pomocja została dodana";
                return RedirectToAction(nameof(Index));
            }
            return View(discountAdd);
        }

        public IActionResult Edit(int id)
        {
            var dicount = _discountService.GetById(id);
            if (dicount == null)
            {
                return NotFound();
            }
            DiscountCreate model = new DiscountCreate
            {
                DiscountId = dicount.DiscountId,
                ProductId = dicount.ProductId,
                Name = dicount.Name,
                Value = dicount.Value,
                BeginDate = dicount.BeginDate,
                FinishDate = dicount.FinishDate,
                Type = dicount.Type,
                TypeOfDiscount = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "0", Text = "Wartościowa" },
                        new SelectListItem { Value = "1", Text = "Procentowa" }
                    }
            };

            ViewData["ProductId"] = new SelectList(_productService.GetAllProducts(), "ProductId", "Name");

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Discount dicount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _discountService.Edit(dicount);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(dicount);
        }

        public IActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var discount = await _context.Discounts
            //    .FirstOrDefaultAsync(m => m.DiscountId == id);
            //if (discount == null)
            //{
            //    return NotFound();
            //}

            //return View(discount);

            try
            {
                //_productService.Delete(id);
                _discountService.Delete(id);
                TempData["Info"] = "Zniżka została usunięta";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania zniżki.";
                throw;
            }

            return RedirectToAction("Index", "Discount");
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
