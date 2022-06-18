using BRO.Domain.IServices;
using BRO.Domain.Query.DTO.OrderDetails;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Utilities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Infrastructure.Services
{
    public class StripeService : IPaymentService
    {
        public async Task<Result> Pay(double price, string email, string successUrl, string cancelUrl)
        {
            var paymentItems = new List<SessionLineItemOptions>();
            paymentItems.Add(new SessionLineItemOptions() { Name = "Zamówienie", Amount = (long)(price * 100), Currency = "pln", Quantity = 1 });
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>{"p24","card",},
                LineItems = paymentItems,
                Mode = "payment",
                CustomerEmail=email, 
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl, 
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return Result.Ok(session.Url);
        }
        public async Task<Result> ReturnPaymentId(string checkoutId)
        {
            var sessionService = new SessionService();
            Session session = await sessionService.GetAsync(checkoutId);
            if (session == null)
                Result.Fail();
            return Result.Ok(session.PaymentIntentId);
        }
    }
}
