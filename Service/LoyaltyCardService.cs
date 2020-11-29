using GasStation.Models;
using GasStation.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class LoyaltyCardService
    {
        private readonly AppDbContext _context;

        public LoyaltyCardService(AppDbContext context)
        {
            _context = context;
        }

        public LoyaltyCard GetById(int id)
        {
            return _context.LoyaltyCards.Where(x => x.LoyaltyCardId == id).FirstOrDefault();
        }
        public IEnumerable<LoyaltyCard> GetAllLoyaltyCards()
        {
            return _context.LoyaltyCards;
        }

        public void Create(LoyaltyCard loyaltyCard)
        {
            try
            {
                _context.Add(loyaltyCard);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public void Edit(LoyaltyCard loyaltyCard)
        {
            try
            {
                _context.Update(loyaltyCard);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public void Delete(int loyaltyCardId)
        {
            try
            {

                var loyaltyCard = GetById(loyaltyCardId);
                _context.Remove(loyaltyCard);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public bool LoyaltyCardExists(int id)
        {
            return _context.LoyaltyCards.Any(e => e.LoyaltyCardId == id);
        }
    }
}