using GasStation.Models;
using GasStation.Models.Contexts;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Service
{
    public class AccountService
    {

        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        
        public AccountService(AppDbContext appDbContext, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public IEnumerable<IdentityUser> GetAllAccounts()
        {
            return _userManager.Users.Cast<ApplicationUser>();
        }
    }
}
