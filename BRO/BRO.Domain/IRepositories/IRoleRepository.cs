using BRO.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IRoleRepository:IRepositoryAsync<Role>
    {
        Task<bool> RoleExistsAsync(string name);
    }
}
