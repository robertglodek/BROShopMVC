using BRO.Domain.Entities;
using BRO.Infrastucture.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,Role,Guid,IdentityUserClaim<Guid>,UserRole,IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>,IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Taste> Tastes { get; set; }
        public DbSet<ProductTaste> ProductTastes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCarts { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderHeader> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderBill> Bills { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureEntities();
            
        }
    }
}
