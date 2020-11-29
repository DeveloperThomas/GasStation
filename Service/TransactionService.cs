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

        public TransactionService(AppDbContext context, AccountService accountService, ProductsListService productsListService)
        {
            _context = context;
            _accountService = accountService;
            _productsListService = productsListService;
        }

        public Transaction GetById(int id)
        {
            return _context.Transactions
                .Include(t => t.ApplicationUser)
                .Include(t => t.LoyaltyCard)
                .FirstOrDefault(m => m.TransactionId == id);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.Include(t => t.ApplicationUser).Include(t => t.LoyaltyCard);
        }

        public void Create(TransactionCreate transactionCreate)
        {
            try
            {
                Transaction transaction = transactionCreate.Transaction;
                transaction.ApplicationUserId = _accountService.GetCurrentUserId();
                transaction.ProductsLists = new List<ProductsList>();

                _context.Add(transaction);
                _context.SaveChanges();


                foreach (var productId in transactionCreate.ProductIds)
                {
                    var productList = _productsListService.CreateProductsLists(transaction.TransactionId, productId, 1);
                    transaction.ProductsLists.Add(productList);
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
