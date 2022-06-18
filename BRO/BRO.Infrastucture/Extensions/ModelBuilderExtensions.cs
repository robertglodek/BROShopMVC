using BRO.Domain.Entities;
using BRO.Domain.Utilities.StaticDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastucture.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEntities(this ModelBuilder builder)
        {
            builder.Entity<Product>(n =>
            {
                n.HasKey(x => x.Id);     
                n.HasOne(n => n.Category).WithMany(n=>n.Products).HasForeignKey(n => n.CategoryId).IsRequired();
                n.HasOne(n => n.Manufacturer).WithMany(n => n.Products).HasForeignKey(n => n.ManufacturerId).IsRequired();  
               
            });
            builder.Entity<DiscountCode>(n =>
            {
                n.HasKey(x => x.Id);             
            });
            builder.Entity<OrderHeader>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.User).WithMany(n => n.Orders).HasForeignKey(n => n.UserId).IsRequired();
                b.HasOne(x => x.OrderBill).WithOne(n => n.Order).HasForeignKey<OrderHeader>(n => n.OrderBillId);
                b.HasOne(x => x.DiscountCode).WithMany(n => n.Orders).HasForeignKey(n => n.DiscountCodeId);

            });
            builder.Entity<Category>(n =>
            {
                n.HasKey(x => x.Id); 
            });
            builder.Entity<Taste>(n =>
            {
                n.HasKey(x => x.Id);  
            });
            builder.Entity<Manufacturer>(n =>
            {
                n.HasKey(x => x.Id); 
            });
            builder.Entity<OrderDetails>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.ProductTaste).WithMany(n => n.OrderDetails).HasForeignKey(n => n.ProductTasteId).IsRequired();
                b.HasOne(x => x.OrderHeader).WithMany(n => n.OrderDetails).HasForeignKey(n => n.OrderHeaderId).IsRequired();
            });
            builder.Entity<ProductTaste>(n =>
            {
                n.HasKey(x => x.Id);
                n.HasOne(x => x.Taste).WithMany(n => n.ProductTastes).HasForeignKey(n => n.TasteId).IsRequired();
                n.HasOne(x => x.Product).WithMany(n => n.ProductTastes).HasForeignKey(n => n.ProductId).IsRequired();
            });
            builder.Entity<ShoppingCartItem>(n =>
            {
                n.HasKey(x => x.Id);
                n.HasOne(n => n.ProductTaste).WithMany(n => n.ShoppingCartItems).HasForeignKey(n => n.ProductTasteId).IsRequired();
                n.HasOne(n => n.ApplicationUser).WithMany(n => n.ShoppingCartItems).HasForeignKey(n => n.ApplicationUserId).IsRequired();                 
            });
            builder.Entity<Review>(n =>
            {
                n.HasKey(x => new { x.ProductId, x.UserId });
                n.HasOne(n => n.Product).WithMany(n => n.Reviews).HasForeignKey(n => n.ProductId).IsRequired();
                n.HasOne(n => n.User).WithMany(n => n.Reviews).HasForeignKey(n => n.UserId).IsRequired();
            });
            builder.Entity<Comment>(n =>
            {
                n.HasKey(x => x.Id); 
                n.HasOne(n => n.Product).WithMany(n => n.Comments).HasForeignKey(n => n.ProductId).IsRequired();
                n.HasOne(n => n.User).WithMany(n => n.Comments).HasForeignKey(n => n.UserId).IsRequired();
            });
            builder.Entity<UserRole>(b =>
            {
                b.HasOne(e => e.User)
                    .WithMany(e => e.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
                b.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            });
           
            builder.Entity<OrderBill>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Order).WithOne(n => n.OrderBill).HasForeignKey<OrderHeader>(n => n.OrderBillId);
            });
        }
    }
}
