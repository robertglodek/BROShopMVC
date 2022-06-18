using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.ApplicationUser;
using BRO.Domain.Query.DTO.Pagination;
using BRO.Domain.Utilities.CustomExceptions;
using BRO.Domain.Utilities.PaginationSearchByRules;
using BRO.Domain.Utilities.PaginationSortingRules;
using BRO.Infrastructure.Repositories;
using BRO.Infrastucture.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Repositories
{
   
    public class ApplicationUserRepository:RepositoryAsync<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<BRO.Domain.Entities.ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApplicationUserRepository(ApplicationDbContext db,
            UserManager<BRO.Domain.Entities.ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
           
        }
        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser entity, string role)
        {
            return await _userManager.AddToRoleAsync(entity, role);
        }
        public async Task<IdentityResult> ChangeEmailAsync(ApplicationUser entity, string newEmail, string token)
        {
            return await _userManager.ChangeEmailAsync(entity, newEmail, token);
        }
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser entity, string password, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(entity, password, newPassword);
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser entity, string password)
        {
            return await _userManager.CheckPasswordAsync(entity, password);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser entity, string token)
        {
            return await _userManager.ConfirmEmailAsync(entity, token);
        }
        public async Task<string> GenerateEmailChangeTokenAsync(ApplicationUser entity, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(entity, newEmail);
        }
        public async Task<string> GenerateEmailConfirmTokenAsync(ApplicationUser entity)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(entity);
        }
        public async  Task<string> GeneratePasswordResetTokenAsync(ApplicationUser entity)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(entity);
        }
        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);
        }
        public async Task SignInAsync(ApplicationUser entity, bool rememberMe)
        {
             await _signInManager.SignInAsync(entity, rememberMe);
        }
        public async Task<IdentityResult> ResetPassowrdAsync(ApplicationUser entity, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(entity, token, newPassword);
        }
        public async Task<PagedResult<ApplicationUser>> SearchAsync<T,T1>(SearchApplicationUsersQuery query,
            Expression<Func<ApplicationUser, IEnumerable<T>>> include = null, Expression<Func<T, T1>> thenInclude = null, string otherPropertiesToInclude = null)
        {
            IQueryable<ApplicationUser> baseQuery = _db.Users;
            baseQuery = baseQuery.Include(include).ThenInclude(thenInclude);
            if (otherPropertiesToInclude != null)
            {
                foreach (var includeProp in otherPropertiesToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    baseQuery = baseQuery.Include(includeProp);
            }
            if (query.SearchName != null)
            {
                if (query.SearchValue == null)
                    query.SearchValue = "";
                var searchSelectors = new ApplicationUserSearchByRules(query.SearchValue.ToLower()).SearchByDictionary;
                if (searchSelectors.ContainsKey(query.SearchName.ToLower()))
                {
                    var selectedSearch = searchSelectors[query.SearchName.ToLower()];
                    baseQuery = baseQuery.Where(selectedSearch);
                }
                else
                    throw new NotFoundException($"SearchName with value: {query.SearchName} does not exist");
            }
            if (query.SortBy != "--")
            {
                var sortPhrase = query.SortBy.Split(":")[0];
                var sortDir = query.SortBy.Split(":")[1];
                var columnsSelectors = new ApplicationUserSortingRules().SortDictionary;
                if (columnsSelectors.ContainsKey(sortPhrase.ToLower()))
                {
                    var selectedOrderBy = columnsSelectors[sortPhrase];
                    var sortingResult = ReturnSorted(sortDir, selectedOrderBy, baseQuery);
                    if (sortingResult != null)
                        baseQuery = sortingResult;
                }
                else
                    throw new NotFoundException($"SortBy with value: {query.SearchName} does not exist");
            }
            var users = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize).ToListAsync();
            var totalElementsCount = await baseQuery.CountAsync();
            var result = new PagedResult<ApplicationUser>(users, totalElementsCount, query.PageSize, query.PageNumber, query.SortBy);
            return result;
        }
        public IQueryable<ApplicationUser> ReturnSorted<T>(string sortDirection, Expression<Func<ApplicationUser, T>> expression, IQueryable<ApplicationUser> elements)
        {
            if (sortDirection == "asc")
                return elements = elements.OrderBy(expression);
            else if (sortDirection == "desc")
                return elements = elements.OrderByDescending(expression);
            return null;
        }





        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            
        }
        public async Task<IdentityResult> LockoutAsync(ApplicationUser user,DateTimeOffset? lockoutEnd)
        {
            return await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser entity,string password)
        {
            return await _userManager.CreateAsync(entity,password);
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<ApplicationUser> GetByEmail(string email, string includeProperties = null)
        {
            IQueryable<ApplicationUser> baseQuery = _db.Users;
            var query=_db.Users.Where(n => n.Email == email);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetByEmailWithDetails(string email)
        {

            IQueryable<ApplicationUser> baseQuery = _db.Users;
            baseQuery = _db.Users.Where(n => n.Email.ToLower() == email.ToLower());
            baseQuery = baseQuery.Include(n => n.UserRoles).ThenInclude(s => s.Role);        
            return await baseQuery.FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetById(Guid Id, string includeProperties = null)
        {
            IQueryable<ApplicationUser> query = _db.Users;
            query = query.Where(n => n.Id == Id);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }

       
    
}
