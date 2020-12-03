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
        private readonly DistributorService _distributorService;
        private readonly TankService _tankService;
        private readonly DiscountService _discountService;

        public TransactionController(TransactionService transactionService,
            AccountService accountService,
            LoyaltyCardService loyaltyCardService,
            ProductService productService,
            ProductsListService productsListService,
            DistributorService distributorService,
            TankService tankService,
            DiscountService discountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            _loyaltyCardService = loyaltyCardService;
            _productService = productService;
            _productsListService = productsListService;
            _distributorService = distributorService;
            _tankService = tankService;
            _discountService = discountService;
        }

        // GET: Transaction
        public IActionResult Index()
        {
            var transactions = _transactionService.GetAllTransactions();
            List<TransactionCreate> transactionListView = new List<TransactionCreate>();
            foreach (var item in transactions)
            {
                TransactionCreate model = new TransactionCreate
                {
                    Transaction = item
                };

                if (item.PaymentType==0)
                    model.NameOfPayment = "Gotówka";
                else
                    model.NameOfPayment = "Karta";

                transactionListView.Add(model);
            }

            return View(transactionListView);
        }

        // GET: Transaction/Details/5
        public IActionResult Details(int id)
        {
            var transaction = _transactionService.GetById(id);
            
            if (transaction == null)
                return NotFound();


            var modelDetail = new TransactionView
            {
                TransactionId = transaction.TransactionId,
                ApplicationUser = transaction.ApplicationUser,
                ApplicationUserId = transaction.ApplicationUserId,
                LoyaltyCard = transaction.LoyaltyCard,
                Date = transaction.Date,
                PaymentConfirmationRequested = transaction.PaymentConfirmationRequested,

            };

            if (transaction.PaymentType == 0)
                modelDetail.NameOfPayment = "Gotówka";
            else
                modelDetail.NameOfPayment = "Karta";

            modelDetail.ProductsLists = _productsListService.GetProductsListsByTransactionId(id);

            return View(modelDetail);
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
            Invoice last = _transactionService.LastInvoice();
            transactionCreate.Transaction = new Transaction();
            transactionCreate.Transaction.Invoice = new Invoice();

            transactionCreate.Transaction.Invoice.InvoiceNumber = last.InvoiceNumber + 1;
        
            foreach (var product in _productService.GetAllProductsWithoutFuel().ToList())
            {
                TransactionProduct model = new TransactionProduct
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    LoyaltyPointsPrice = product.LoyaltyPointsPrice,
                    IsDiscountIncluded = false,
                    MaxAmountOfProduct= product.Stock
                };

                Discount discount = _discountService.GetDiscountForProduct(model.ProductId);
                
                if (discount != null)
                {
                    switch (discount.Type)
                    {
                        case 0:
                            model.IsDiscountIncluded = true;
                            break;
                        case 1:
                            model.IsDiscountIncluded = true;
                            break;
                        default:
                            model.IsDiscountIncluded = false;
                            break;
                    }
                }

                transactionCreate.TransactionProduct.Add(model);
            }
            transactionCreate.DistributorInTransaction = new List<DistributorInTransaction>();

            foreach (var distributor in _distributorService.GetAllDistributors().ToList())
            {
                DistributorInTransaction model = new DistributorInTransaction();

                List<TankDistributor> listOfTankDistributor = _tankService.GetTanksByDistributor(distributor.DistributorId).ToList();

                List<int> listOfTanksIds = new List<int>();
                foreach (var item in listOfTankDistributor)
                {
                    listOfTanksIds.Add(item.TankId);
                }
                listOfTanksIds.Sort();
                Random r = new Random();
                int randomTankId =  r.Next(listOfTanksIds[0], listOfTanksIds[^1]);
                Tank tank = _tankService.GetById(randomTankId);

                bool isCorrect = false;

                float pom = 0;

                model.NameOfFuel = tank.Product.Name;
                model.PriceForLiter = tank.Product.Price;
                model.DistributorId = distributor.DistributorId;
                model.TankId = tank.TankId;
                while(isCorrect==false)
                {
                     r = new Random();
                     pom = r.Next(0, 50);
                    if(tank.Stock>=pom)
                    {
                        model.Counter = pom;
                        isCorrect = true;
                    }
                }
              

               
                model.Sum = model.Counter * (float)model.PriceForLiter;
                transactionCreate.DistributorInTransaction.Add(model);
            }

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
            try
            {

                if (transactionCreate.Transaction.LoyaltyCardId == 0) transactionCreate.Transaction.LoyaltyCardId = null;

                if (transactionCreate.Transaction.LoyaltyCardId != null && transactionCreate.Transaction.LoyaltyCardId != 0)
                {
                    LoyaltyCard loyaltyCard = _loyaltyCardService.GetById((int)transactionCreate.Transaction.LoyaltyCardId);
                    if(loyaltyCard.Points< transactionCreate.SumOfLoyaltyCardPoints)
                    {
        
                        TempData["Warning"] = "Klient posiada "+ loyaltyCard.Points +"punktów.";

                        transactionCreate.TypesOfPayment = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "0", Text = "Gotówka" },
                            new SelectListItem { Value = "1", Text = "Karta" }
                        };

                        ViewData["LoyaltyCardId"] = new SelectList(_loyaltyCardService.GetAllLoyaltyCards(), "LoyaltyCardId", "LoyaltyCardId");



                        return View(transactionCreate);
                    }
                }
               


                _transactionService.Create(transactionCreate);
                TempData["Info"] = "Transakcja została dodana.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Warning"] = "Wystąpił błąd podczas dodawania transakcji.";
                throw;
            }
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
