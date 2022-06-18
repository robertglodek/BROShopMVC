using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.OrderDetails;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.IServices
{
    public interface IPaymentService
    {
        Task<Result> Pay(double price,string email,string successUrl,string cancelUrl);
        Task<Result> ReturnPaymentId(string checkoutId);
    }
}
