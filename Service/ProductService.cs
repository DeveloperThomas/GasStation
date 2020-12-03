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
        private readonly DiscountService _discountService;

        public ProductService(AppDbContext context, DiscountService discountService)
        {
            _context = context;
            _discountService = discountService;
        }

        public Product GetById(int id)
        {
            return _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> productsIncludingDiscounts = _context.Products.ToList();

            foreach (Product product in productsIncludingDiscounts)
            {
                Discount discount = _discountService.GetDiscountForProduct(product.ProductId);

                if (discount != null)
                {
                    switch (discount.Type)
                    {
                        case 0:
                            product.Price -= discount.Value;
                            break;
                        case 1:
                            product.Price *= Math.Round((decimal)1.0 - (discount.Value / 100), 2);
                            break;
                        default:
                            break;
                    }
                }
            }

            return _context.Products;
        }
        public IEnumerable<Product> GetAllProductsWithoutFuel()
        {
            return GetAllProducts().Where(x=>x.Is_Fuel==false).ToList();
        }
        public IEnumerable<Product> GetAllProductsForTank()
        {
            return GetAllProducts().Where(x=>x.Is_Fuel==true);
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
        public void Edit(int productId, float amount)
        {
            try
            {
                Product product = GetById(productId);
                product.Stock = product.Stock - amount;

                _context.Update(product);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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
