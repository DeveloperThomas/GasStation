using GasStation.Models;
using GasStation.Models.Contexts;
using GasStation.Models.ModelsView;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        private readonly AccountService _accountService;
        private readonly ProductsListService _productsListService;
        private readonly FuelingService _fuelingService;
        private readonly ProductService _productService;
        private readonly TankService  _tankService;
        private readonly LoyaltyCardService  _loyaltyCardService;

        public TransactionService(AppDbContext context, AccountService accountService, ProductsListService productsListService,
            FuelingService fuelingService, ProductService productService, TankService tankService, LoyaltyCardService loyaltyCardService)
        {
            _context = context;
            _accountService = accountService;
            _productsListService = productsListService;
            _fuelingService = fuelingService;
            _productService = productService;
            _tankService = tankService;
            _loyaltyCardService = loyaltyCardService;
        }

        public Transaction GetById(int id)
        {
            return _context.Transactions
                .Include(t => t.ApplicationUser)
                .Include(t => t.LoyaltyCard)
                .FirstOrDefault(m => m.TransactionId == id);
        }
        public List<Transaction> GetTransactionsBetweenDates(DateTime dateFrom, DateTime dataTo)
        {
            return _context.Transactions.Where(x => x.Date >= dateFrom && x.Date <= dataTo).Include(x=>x.ProductsLists).Include(y=>y.Invoice).ToList();
        }
        public Invoice LastInvoice()
        {
            return _context.Invoices.ToList().Last();
        }
            public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.Include(t => t.ApplicationUser).Include(t => t.LoyaltyCard);
        }

        public void Create(TransactionCreate transactionCreate)
        {
            try
            {
                if (transactionCreate.SumOfLoyaltyCardPoints > 0 && transactionCreate.Transaction.LoyaltyCardId != null &&
                  transactionCreate.Transaction.LoyaltyCardId != 0)
                {
                    LoyaltyCard loyaltyCard = _loyaltyCardService.GetById((int)transactionCreate.Transaction.LoyaltyCardId);

                    loyaltyCard.Points -= transactionCreate.SumOfLoyaltyCardPoints;
                    _loyaltyCardService.Edit(loyaltyCard);
                }

                Invoice invoice = transactionCreate.Transaction.Invoice;
                if (invoice.NIP == 0 || invoice.InvoiceNumber == 0 || String.IsNullOrEmpty(invoice.RegistrationNumber) == true)
                    transactionCreate.Transaction.Invoice = null;

                Transaction transaction = transactionCreate.Transaction;
                transaction.ApplicationUserId = _accountService.GetCurrentUserId();
                transaction.ProductsLists = new List<ProductsList>();
                transaction.Date = DateTime.Now;

                _context.Add(transaction);
                _context.SaveChanges();


                foreach (var transactionProduct in transactionCreate.TransactionProduct)
                {

                    if(transactionProduct.InTransaction)
                    {
                        var productList = _productsListService.CreateProductsLists(transaction.TransactionId, transactionProduct.ProductId, transactionProduct.Amount, transactionProduct.Price);
                        transaction.ProductsLists.Add(productList);
          

                        _productService.Edit(transactionProduct.ProductId, transactionProduct.Amount);

                        if(transaction.LoyaltyCardId!=null && transaction.LoyaltyCardId!=0)
                        {
                            Product product = _productService.GetById(transactionProduct.ProductId);
                            LoyaltyCard loyaltyCard = _loyaltyCardService.GetById((int)transaction.LoyaltyCardId);
                            var sumpoints = transactionProduct.Amount * product.LoyaltyPointsPrice;
                            loyaltyCard.Points = (int)Math.Round((decimal)sumpoints);
                            _loyaltyCardService.Edit(loyaltyCard);
                        }

                    }
          
                }
                foreach (var distributor in transactionCreate.DistributorInTransaction)
                {
                    if (distributor.InTransaction)
                    {
                        Fueling fueling = new Fueling
                        {
                            TankId = distributor.TankId,
                            DistributorId = distributor.DistributorId,
                            Date = DateTime.Now,
                            Amount = distributor.Counter,
                            Price = distributor.Sum
                        };
                        _fuelingService.Create(fueling);

                        Tank tank = _tankService.GetById(distributor.TankId);
                        tank.Stock = tank.Stock - distributor.Counter;
                        _tankService.Edit(tank);
                        Product product = _productService.GetById(tank.ProductId);

                        var productList = _productsListService.CreateProductsLists(transaction.TransactionId, product.ProductId, distributor.Counter, distributor.PriceForLiter);
                        transaction.ProductsLists.Add(productList);
                        _productService.Edit(product.ProductId, distributor.Counter);

                        if (transaction.LoyaltyCardId != null && transaction.LoyaltyCardId != 0)
                        {
                           
                            LoyaltyCard loyaltyCard = _loyaltyCardService.GetById((int)transaction.LoyaltyCardId);
                            var sumpoints = distributor.Counter * product.LoyaltyPointsPrice;
                            loyaltyCard.Points = (int)Math.Round((decimal)sumpoints);
                            _loyaltyCardService.Edit(loyaltyCard);
                        }


                    }


                }

           
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Edit(Transaction transaction)
        {
            try
            {
                _context.Update(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int transactionId)
        {
            try
            {
                var transaction = GetById(transactionId);
                _context.Remove(transaction);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }

    }
}
