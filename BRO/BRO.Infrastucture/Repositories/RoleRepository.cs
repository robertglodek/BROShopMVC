using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Infrastructure.Repositories;
using BRO.Infrastucture.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Repositories
{
    public class RoleRepository:RepositoryAsync<Role>,IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly RoleManager<Role> _roleManager;
        public RoleRepository(ApplicationDbContext db,RoleManager<Role> roleManager) : base(db)
        {
            _roleManager = roleManager;
            _db = db;

            
        }
        public async Task<bool> RoleExistsAsync(string name)
        {
            return await _roleManager.RoleExistsAsync(name);
        }

    }
}
