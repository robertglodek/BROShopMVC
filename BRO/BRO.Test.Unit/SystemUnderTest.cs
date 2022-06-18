using BRO.Domain.Entities;
using BRO.Test.Unit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Test.Unit
{
    public class SystemUnderTest : IDisposable
    {
        public Category CreateCategory(string name)
        {
            var category = new CategoryProxy(name);
            return category;
        }
        public Product CreateProduct(string name)
        {
            var category = new ProductProxy(name);
            return category;
        }

        public Category CreateCategoryWithProducs(string name)
        {
            var category = new CategoryProxy(name);
            category.Products.Add(new Product() { Name = "Nowy" });
            category.Products.Add(new Product() { Name = "Nowy2" });
            return category;
        }

        public Taste CreateTaste(string name)
        {
            var taste = new TasteProxy(name);
            return taste;
        }
        public Review CreateReview(string content)
        {
            var review = new ReviewProxy(content);
            return review;
        }
        public Carrier CreateCarrier(string name)
        {
            var carrier = new CarrierProxy(name);
            return carrier;
        }
        public Carrier CreateCarrierWithOrders(string name)
        {
            var carrier = new CarrierProxy(name);
            carrier.Orders = new List<OrderHeader>();
            carrier.Orders.Add(new OrderHeader() { });
            carrier.Orders.Add(new OrderHeader() { });
            return carrier;
        }
        public Comment CreateComment(string content)
        {
            var comment = new CommentProxy(content);
            return comment;
        }
        public Taste CreateTasteWithProducTastes(string name)
        {
            var taste = new TasteProxy(name);
            taste.ProductTastes = new List<ProductTaste>();
            taste.ProductTastes.Add(new ProductTaste());
            return taste;  
        }

        public Manufacturer CreateManufacturer(string name)
        {
            var manufacturer = new ManufacturerProxy(name);
            return manufacturer;
        }

        public Manufacturer CreateManufacturerWithProducs(string name, params string[] productNames)
        {
            var manufacturer = new ManufacturerProxy(name);
            manufacturer.Products = new List<Product>();
            manufacturer.Products.Add(new Product() { Name = "Nowy" });
            manufacturer.Products.Add(new Product() { Name = "Nowy2" });
            return manufacturer;
        }
        public void Dispose()
        {
        }
    }
}
