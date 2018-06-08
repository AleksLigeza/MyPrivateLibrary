using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Interfaces;

namespace MyPrivateLibraryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ApplicationUser> GetUserByEmail(string email)
        {
            return _context.Users.Where(x =>
                    string.Equals(x.Email, email, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefaultAsync();
        }
    }
}
