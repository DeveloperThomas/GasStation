using GasStation.Models;
using GasStation.Models.Contexts;

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

        public TransactionService(AppDbContext context)
        {
            _context = context;
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
            return _context.Transactions;
        }

        public void Create(Transaction transaction)
        {
            try
            {
                _context.Add(transaction);
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
