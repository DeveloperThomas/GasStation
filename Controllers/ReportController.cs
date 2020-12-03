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
        private readonly TransactionService _transactionService;

        public ReportController(ProductService productService, TransactionService transactionService)
        {
            _productService = productService;
            _transactionService = transactionService;


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
        public IActionResult ReportOfSales(ReportSale ReportSale)
        {
            return RedirectToAction("GenerateSales", ReportSale);
        }
        public IActionResult GenerateSales(ReportSale reportSale)
        {
            var transactions = _transactionService.GetTransactionsBetweenDates(reportSale.DateFrom, reportSale.DateTo);

            List<Product> listOfProducts = new List<Product>();

            foreach (var item in transactions)
            {
                foreach (var productList in item.ProductsLists)
                {
                    var productToAdd = _productService.GetById(productList.ProductId);
                    listOfProducts.Add(productToAdd);

                }
           
            }

            IEnumerable<Product> listOfDistinctProducts = listOfProducts.Distinct();


            foreach (var item in listOfDistinctProducts)
            {
                foreach (var product in listOfProducts)
                {
                    if(item.ProductId==product.ProductId)
                    {
                        item.Price += product.Price;
                        item.Stock += 1;
                    }
                }
            }


            reportSale.NumberOfTransactions = transactions.Count();
            reportSale.NumberOfSoldProducts = listOfProducts.Count();
            reportSale.NumberOfInvoices = transactions.Where(x => x.Invoice != null).Count();


            reportSale.Products = listOfDistinctProducts.ToList();


            return View("ReportOfSales", reportSale);
        }

    }
}
