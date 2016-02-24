using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pawze.API.Domain;
using Pawze.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        // Assigned customer role
        public async Task<IdentityResult> RegisterCustomer(RegistrationModel model)
        {
            // create a user
            var pawzeUser = new PawzeUser
            {
                UserName = model.Username,
                Email = model.EmailAddress
            };

            // save the user
            var result = await _userManager.CreateAsync(pawzeUser, model.Password);

            // create the role
            var connectionBetweenUserAndRole = new IdentityUserRole

            {
                RoleId = _db.Roles.FirstOrDefault(r => r.Name == "Customer").Id,
                UserId = pawzeUser.Id
            };

            // attach the role to the user
            pawzeUser.Roles.Add(connectionBetweenUserAndRole);

            // mark the user as modified
            _db.Entry(pawzeUser).State = EntityState.Modified;

            // save changes
            _db.SaveChanges();

            // ?
            // ?

            // ... PROFIT
            return result;
        }
        // Assigned Admin role
        public async Task<IdentityResult> RegisterAdmin(RegistrationModel model)
        {
            // create a user
            var pawzeUser = new PawzeUser
            {
                UserName = model.Username,
                Email = model.EmailAddress
            };

            // save the user
            var result = await _userManager.CreateAsync(pawzeUser, model.Password);

            // create the role
            var connectionBetweenUserAndRole = new IdentityUserRole
            
            {
                RoleId = _db.Roles.FirstOrDefault(r => r.Name == "Admin").Id,
                UserId = pawzeUser.Id
            };

            // attach the role to the user
            pawzeUser.Roles.Add(connectionBetweenUserAndRole);

            // mark the user as modified
            _db.Entry(pawzeUser).State = EntityState.Modified;

            // save changes
            _db.SaveChanges();

            // ?
            // ?

            // ... PROFIT
            return result;
        }
        // Assigned staff role
        public async Task<IdentityResult> RegisterStaff(RegistrationModel model)
        {
            // create a user
            var pawzeUser = new PawzeUser
            {
                UserName = model.Username,
                Email = model.EmailAddress
            };

            // save the user
            var result = await _userManager.CreateAsync(pawzeUser, model.Password);

            // create the role
            var connectionBetweenUserAndRole = new IdentityUserRole

            {
                RoleId = _db.Roles.FirstOrDefault(r => r.Name == "Staff").Id,
                UserId = pawzeUser.Id
            };

            // attach the role to the user
            pawzeUser.Roles.Add(connectionBetweenUserAndRole);

            // mark the user as modified
            _db.Entry(pawzeUser).State = EntityState.Modified;

            // save changes
            _db.SaveChanges();

            // ?
            // ?

            // ... PROFIT
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