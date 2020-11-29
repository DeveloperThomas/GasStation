using GasStation.Models;
using GasStation.Models.Contexts;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class ProductsListService
    {
    
        private readonly AppDbContext _context;
        public ProductsListService(AppDbContext context)
        {
            _context = context;
        }

        public ProductsList CreateProductsLists(int transactionId, int productId, float amount)
        {
            var productsList = new ProductsList()
            {
                TransactionId = transactionId,
                ProductId = productId,
                Amount = amount
            };

            _context.Add(productsList);
            _context.SaveChanges();

            return productsList;
        }

        public List<ProductsList> GetProductsListsByTransactionId(int transactionId)
        {
            return _context.ProductsLists
                .Include(t => t.Transaction)
                .Include(p => p.Product)
                .Where(pl => pl.TransactionId == transactionId).ToList();
        }
    }
}
