using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Infrastucture.Data;
using BRO.Infrastucture.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            CategoryRepository = new CategoryRepository(_context);
            TasteRepository = new TasteRepository(_context);
            ManufacturerRepository = new ManufacturerRepository(_context);
            ProductRepository = new ProductRepository(_context);
            ProductTasteRepository = new ProductTasteRepository(_context);
            ShoppingCartItemRepository = new ShoppingCartItemRepository(_context);
            ApplicationUserRepository = new ApplicationUserRepository(_context, _userManager,signInManager);
            RoleRepository = new RoleRepository(_context, roleManager);
            OrderRepository = new OrderHeaderRepository(_context);
            OrderDetailsRepository = new OrderDetailsRepository(_context);
            CarrierRepository = new CarrierRepository(_context);
            CommentRepository = new CommentRepository(_context);
            ReviewRepository = new ReviewRepository(_context);
            OrderBillRepository = new OrderBillRepository(_context);
            DiscountCodeRepository = new DiscountCodeRepository(_context);

        }
        public IOrderBillRepository OrderBillRepository { get; }
        public ICarrierRepository CarrierRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ITasteRepository TasteRepository { get; }
        public IManufacturerRepository ManufacturerRepository { get; }
        public IProductTasteRepository ProductTasteRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IShoppingCartItemRepository ShoppingCartItemRepository { get; }
        public IApplicationUserRepository ApplicationUserRepository { get; }
        public IOrderHeaderRepository OrderRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; set; }
        public ICommentRepository CommentRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IDiscountCodeRepository DiscountCodeRepository { get; }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
