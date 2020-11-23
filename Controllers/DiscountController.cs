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
        public DiscountController(DiscountService discountService, ProductService productService)
        {

            _discountService = discountService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_discountService.GetAllDiscounts());
        }


        public IActionResult Create()
        {
            DiscountCreate model = new DiscountCreate();
            model.TypeOfDiscount = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "0", Text = "Wartościowa" },
                        new SelectListItem { Value = "1", Text = "Procentowa" },
      
                    };
            ViewData["ProductId"] = new SelectList(_productService.GetAllProducts(), "ProductId", "Name");
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
                        new SelectListItem { Value = "1", Text = "Procentowa" },

                    }
            };
   
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, Discount dicount)
        {
            if (id != dicount.ProductId)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
