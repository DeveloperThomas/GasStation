using GasStation.Models;
using GasStation.Models.Contexts;

using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Security.Claims;

namespace GasStation.Service
{
    public class AccountService
    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<ApplicationUser> GetAllAccounts()
        {
            return _context.Users;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
