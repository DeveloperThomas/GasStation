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
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly AccountService _accountService;
        private readonly LoyaltyCardService _loyaltyCardService;
        private readonly ProductService _productService;
        private readonly ProductsListService _productsListService;

        public TransactionController(TransactionService transactionService,
            AccountService accountService,
            LoyaltyCardService loyaltyCardService,
            ProductService productService,
            ProductsListService productsListService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _loyaltyCardService = loyaltyCardService;
            _productService = productService;
            _productsListService = productsListService;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            return View(_transactionService.GetAllTransactions());
        }

        // GET: Transaction/Details/5
        public IActionResult Details(int id)
        {
            var transaction = _transactionService.GetById(id);

            if (transaction == null)
                return NotFound();

            transaction.ProductsLists = _productsListService.GetProductsListsByTransactionId(id);

            return View(transaction);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            TransactionCreate transactionCreate = new TransactionCreate()
            {
                TypesOfPayment = new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = "Gotówka" },
                    new SelectListItem { Value = "1", Text = "Karta" }
                }
            };

            transactionCreate.TransactionProduct = new List<TransactionProduct>();

            foreach (var product in _productService.GetAllProducts().ToList())
                transactionCreate.TransactionProduct.Add(new TransactionProduct(product, 0));

            ViewData["LoyaltyCardId"] = new SelectList(_loyaltyCardService.GetAllLoyaltyCards(), "LoyaltyCardId", "LoyaltyCardId");

            return View(transactionCreate);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("TransactionId,ApplicationUserId,LoyaltyCardId,Date,PaymentType,PaymentConfirmationRequested")] Transaction transaction)
        public IActionResult Create(TransactionCreate transactionCreate)
        {
            if (ModelState.IsValid)
            {
                _transactionService.Create(transactionCreate);
                return RedirectToAction(nameof(Index));
            }

            return View(transactionCreate);
        }

        // GET: Transaction/Edit/5
        public IActionResult Edit(int id)
        {

            var transaction = _transactionService.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            ViewData["ProductIds"] = new SelectList(_productService.GetAllProducts(), "ProductId", "Name");
            ViewData["LoyaltyCardId"] = new SelectList(_loyaltyCardService.GetAllLoyaltyCards(), "LoyaltyCardId", "City");
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _transactionService.Edit(transaction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        private bool TransactionExists(int id)
        {
            return _transactionService.TransactionExists(id);
        }
    }
}
