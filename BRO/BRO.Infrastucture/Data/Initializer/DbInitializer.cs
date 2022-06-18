using BRO.Domain.Entities;
using BRO.Domain.Utilities.StaticDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            if(_db.Database.CanConnect())
            {
                var pendingMigrations=_db.Database.GetPendingMigrations();
                if(pendingMigrations!=null && pendingMigrations.Any())
                    _db.Database.Migrate();

                if (!_db.Roles.Any())
                {
                    _db.Roles.AddRangeAsync(GetRoles()).GetAwaiter().GetResult();
                    _db.SaveChangesAsync().GetAwaiter().GetResult();

                }
                    

                var existingUser= _db.Users.FirstOrDefaultAsync(n => n.Email == "youtube1924@o2.pl").GetAwaiter().GetResult();
                if (existingUser == null)
                {
                    _userManager.CreateAsync(new ApplicationUser() { FirstName = "Admin", Email = "sample_email@gmail.com", UserName= "sample_email@gmail.com", EmailConfirmed=true }, "Admin123@").GetAwaiter().GetResult();
                    var newUser = _db.Users.FirstOrDefaultAsync(n => n.Email == "sample_email@gmail.com").GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(newUser, Roles.RoleAdmin).GetAwaiter().GetResult();
                    
                }     
            }
        }
        public IEnumerable<Role> GetRoles()
        {
            var roleAdmin = new Role() { ConcurrencyStamp = Guid.NewGuid().ToString(), Name = Roles.RoleAdmin, NormalizedName = Roles.RoleAdmin.ToUpper() };
            var roleEmployee = new Role() { ConcurrencyStamp = Guid.NewGuid().ToString(), Name = Roles.RoleEmployee, NormalizedName = Roles.RoleEmployee.ToUpper() };
            var roleUserIndividual = new Role() { ConcurrencyStamp = Guid.NewGuid().ToString(), Name = Roles.RoleUserIndividual, NormalizedName = Roles.RoleUserIndividual.ToUpper() };

            return new List<Role>() { roleAdmin, roleEmployee, roleUserIndividual };
        }

    }
}
