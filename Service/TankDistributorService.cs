using GasStation.Models;
using GasStation.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class TankDistributorService
    {
        private readonly AppDbContext _context;

        public TankDistributorService(AppDbContext context)
        {
            _context = context;
        }

        public TankDistributor GetById(int idTank, int idDistributor)
        {
            return _context.TankDistributors.Where(x => x.DistributorId == idDistributor && x.TankId == idTank).FirstOrDefault();
        }
        public IEnumerable<TankDistributor> GetAllTankDistributors()
        {
            return _context.TankDistributors;
        }

        public TankDistributor Create(int tankId, int distributorId)
        {
            try
            {
                TankDistributor td = new TankDistributor()
                {
                    TankId = tankId,
                    DistributorId = distributorId
                };
                _context.Add(td);
                _context.SaveChanges();

                return td;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void Edit(TankDistributor tankdistributor)
        {
            try
            {
                _context.Update(tankdistributor);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
        public void Delete(int idTank, int idDistributor)
        {
            try
            {

                var tankdistributor = GetById(idTank, idDistributor);
                _context.Remove(tankdistributor);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public bool TankDistributorExists(int idTank, int idDistributor)
        {
            return _context.TankDistributors.Any(e => e.DistributorId == idDistributor && e.TankId == idTank);
        }
    }
}
