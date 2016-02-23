using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pawze.API.Domain;
using Pawze.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pawze.API.Infrastructure
{
    public class AuthorizationRepository : IDisposable
    {
        private PawzeDataContext _db;
        private UserManager<PawzeUser> _userManager;

        public AuthorizationRepository()
        {
            _db = new PawzeDataContext();
            _userManager = new UserManager<PawzeUser>(new UserStore<PawzeUser>(_db));
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var pawzeUser = new PawzeUser
            {
                UserName = model.Username,
                Email = model.EmailAddress
            };

            var result = await _userManager.CreateAsync(pawzeUser, model.Password);
            return result;
        }

        public async Task<PawzeUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }


        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}