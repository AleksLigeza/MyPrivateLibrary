using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPrivateLibraryAPI.DbModels;

namespace MyPrivateLibraryAPI.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
