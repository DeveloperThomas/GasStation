using GasStation.Models;
using GasStation.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public Product GetById(int id)
        {
            return _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }
        public IEnumerable<Product> GetAllProductsWithoutFuel()
        {
            return _context.Products.Where(x=>x.Is_Fuel==false).ToList();
        }
        public IEnumerable<Product> GetAllProductsForTank()
        {
            return _context.Products.Where(x=>x.Is_Fuel==true);
        }

        public IEnumerable<Product> GetProductsBeetwenDates(DateTime dateFrom, DateTime dateTo)
        {
            //List<Transaction> 
            //return _context.Products.Where(x => x.date>= dateFrom && x.Date <= dataTo).ToList();
            return new List<Product>();
        }
        public void Create(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public void Edit(Product product)
        {
            try
            {
                _context.Update(product);
                _context.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw new Exception( ex.ToString());
            }

        }
        public void Delete(int productId)
        {
            try
            {

                var product = GetById(productId);
                _context.Remove(product);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
