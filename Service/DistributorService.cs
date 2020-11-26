using GasStation.Models;
using GasStation.Models.Contexts;
using GasStation.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class DistributorService
    {
        private readonly AppDbContext _context;
        private readonly TankDistributorService _tankDistributorService;
        private readonly TankService _tankService;

        public DistributorService(AppDbContext context, TankDistributorService tankDistributorService, TankService tankService)
        {
            _context = context;
            _tankDistributorService = tankDistributorService;
            _tankService = tankService;
        }

        public Distributor GetById(int id)
        {
            return _context.Distributors.Where(x => x.DistributorId == id).FirstOrDefault();
        }
        public IEnumerable<Distributor> GetAllDistributors()
        {
            return _context.Distributors;
        }

        public void Create(DistributorCreate distributorCreate)
        {
            try
            {
                Distributor distributorToAdd = new Distributor
                {
                    IsLocked = false,
                    Counter = 0,
                    ProductId = 1,
                    TankDistributors = new List<TankDistributor>()
                };

                _context.Add(distributorToAdd);
                _context.SaveChanges();

                if (distributorCreate.TankIds == null) distributorCreate.TankIds = new List<int>();

                foreach (int tankId in distributorCreate.TankIds)
                {
                    TankDistributor td = _tankDistributorService.Create(tankId, distributorToAdd.DistributorId);
                    distributorToAdd.TankDistributors.Add(td);
                    _tankService.GetById(tankId).TankDistributors.Add(td);
                }

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
