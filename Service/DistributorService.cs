using GasStation.Models;
using GasStation.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class DistributorService
    {
        private readonly AppDbContext _context;

        public DistributorService(AppDbContext context)
        {
            _context = context;
        }

        public Distributor GetById(int id)
        {
            return _context.Distributors.Where(x => x.DistributorId == id).FirstOrDefault();
        }
        public IEnumerable<Distributor> GetAllDistributors()
        {
            return _context.Distributors;
        }

        public void Create(Distributor distributor)
        {
            try
            {
                _context.Add(distributor);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public void Edit(Distributor distributor)
        {
            try
            {
                _context.Update(distributor);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public void Delete(int distributorId)
        {
            try
            {

                var distributor = GetById(distributorId);
                _context.Remove(distributor);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public bool DistributorExists(int id)
        {
            return _context.Distributors.Any(e => e.DistributorId == id);
        }
    }
}
