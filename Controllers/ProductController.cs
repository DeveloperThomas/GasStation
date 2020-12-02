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

namespace GasStation.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ProductService _productService;

        public ProductController(AppDbContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetAllProducts());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
               
                _productService.Create(product);
                TempData["Info"] = "Produkt został dodany";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {


            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public  IActionResult Edit(int id,  Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.Edit(product);
                    TempData["Info"] = "Dane produktu zostały zaktualizowane";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productService.ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Warning"] = "Wystąpił błąd podczas edycji produktu";
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                TempData["Info"] = "Produkt został usunięty";
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystapił błąd podczas usuwania produktu.";
                throw;
            }

            return RedirectToAction("Index", "Product");
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
