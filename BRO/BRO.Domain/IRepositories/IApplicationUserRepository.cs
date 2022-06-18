using BRO.Domain.Entities;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.DTO.Pagination;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IRepositories
{
    public interface IApplicationUserRepository : IRepositoryAsync<ApplicationUser>
    {
        Task<PagedResult<ApplicationUser>> SearchAsync<T,T1>(SearchApplicationUsersQuery query,
            Expression<Func<ApplicationUser, IEnumerable<T>>> include = null, Expression<Func<T, T1>> thenInclude = null, string otherPropertiesToInclude = null);
        Task UpdateAsync(ApplicationUser user);
        Task<IdentityResult>  CreateAsync(ApplicationUser entity,string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser entity,string role);
        Task<string> GenerateEmailChangeTokenAsync(ApplicationUser entity, string newEmail);
        Task<string> GenerateEmailConfirmTokenAsync(ApplicationUser entity);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser entity);
        Task<IdentityResult> ResetPassowrdAsync(ApplicationUser entity,string token,string newPassword);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser entity, string token);
        Task<IdentityResult>  ChangeEmailAsync(ApplicationUser entity, string newEmail,string token);
        Task<bool> CheckPasswordAsync(ApplicationUser entity, string password);
        Task<SignInResult> PasswordSignInAsync(string email, string password,bool rememberMe,bool lockoutOnFailure);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser entity, string password, string newPassword);
        Task SignOutAsync();
        Task<IdentityResult> LockoutAsync(ApplicationUser user, DateTimeOffset? lockoutEnd);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task SignInAsync(ApplicationUser entity, bool rememberMe);
        Task<ApplicationUser> GetByEmail(string email, string includeProperties = null);
        Task<ApplicationUser> GetByEmailWithDetails(string email);
        Task<ApplicationUser> GetById(Guid Id, string includeProperties = null);
    }
}
