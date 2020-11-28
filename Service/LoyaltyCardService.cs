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

        public IEnumerable<LoyaltyCard> GetAllLoyaltyCards()
        {
            return _context.LoyaltyCards;
        }
    }
}
