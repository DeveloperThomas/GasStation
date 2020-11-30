using GasStation.Models;
using GasStation.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class TankService
    {
        private readonly AppDbContext _context;

        public TankService(AppDbContext context)
        {
            _context = context;
        }

        public Tank GetById(int id)
        {
            return _context.Tanks.Where(x => x.TankId == id).Include(item=>item.Product).FirstOrDefault();
        }
        public IEnumerable<Tank> GetAllTanks()
        {
            return _context.Tanks.Include(x=>x.Product);
        }
        public IEnumerable<TankDistributor> GetTanksByDistributor(int distributorId)
        {
            return _context.TankDistributors.Where(x => x.DistributorId == distributorId).ToList();
        }
        
        public void Create(Tank tank)
        {
            try
            {
                _context.Add(tank);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public void Edit(Tank tank)
        {
            try
            {
                _context.Update(tank);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public void Delete(int tankId)
        {
            try
            {

                var tank = GetById(tankId);
                _context.Remove(tank);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }

        }
        public bool TankExists(int id)
        {
            return _context.Tanks.Any(e => e.TankId == id);
        }
    }
}
