using GasStation.Models;
using GasStation.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class FuelingService
    {
        private readonly AppDbContext _context;

        public FuelingService(AppDbContext context)
        {
            _context = context;
        }

        public List<Fueling> GetFuelingBeetwenDates(DateTime dateFrom,DateTime dataTo)
        {
            return _context.Fuelings.Where(x=>x.Date>= dateFrom && x.Date<= dataTo).ToList();
        }
        public void Create(Fueling fueling)
        {
            try
            {
                _context.Add(fueling);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
    }
}
