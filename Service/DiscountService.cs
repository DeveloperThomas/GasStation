using GasStation.Models;
using GasStation.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class DiscountService
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }

        public Discount GetById(int id)
        {
            return _context.Discounts.Where(x => x.DiscountId == id).FirstOrDefault();
        }
        public IEnumerable<Discount> GetAllDiscounts()
        {
            return _context.Discounts.Include(x=>x.Product);
        }

        public void Create(Discount discount)
        {
            try
            {
                _context.Add(discount);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public void Edit(Discount discount)
        {
            try
            {
                _context.Update(discount);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public void Delete(int discountId)
        {
            try
            {

                var product = GetById(discountId);
                _context.Remove(discountId);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
    }
}
