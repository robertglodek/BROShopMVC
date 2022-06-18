using AutoMapper;
using BRO.Domain.Command.OrderBill;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Utilities;
using BRO.Domain.Utilities.StaticDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderHeader
{
    public class AddOrderCommandHandler:ICommandHandler<AddOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddOrderCommand command)
        {
            var validationResult = await new AddOrderCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            if (command.IsOrderBill==true && command.OrderBillCommand!=null)
            {
                var validationResultBill = await new AddOrderBillCommandValidator().ValidateAsync(command.OrderBillCommand);
                if(!validationResultBill.IsValid)
                    return Result.Fail(validationResultBill);
            }
            var order = _mapper.Map<BRO.Domain.Entities.OrderHeader>(command);
            order.OrderProductsTotal = 0;
            order.OrderShippingCost = 0;
            order.DiscountInPercent = 0;
            order.OrderDate = DateTimeOffset.Now;
            order.PaymentStatus = PaymentStatus.PaymentStatusPending;
            order.OrderStatus = OrderStatus.OrderStatusPending;

            var user = await _unitOfWork.ApplicationUserRepository.GetById(command.UserId);
            if(user==null)
                return Result.Fail("Nie udało się złożyć zamówienia");
            var carrier = await _unitOfWork.CarrierRepository.GetById(command.CarrierId);
            if (carrier == null)
                return Result.Fail("Nie udało się złożyć zamówienia");

            var shoppingCart = await _unitOfWork.ShoppingCartItemRepository.GetAllByUserIdAsync(command.UserId);
            var orderDetails = new List<OrderDetails>();
            foreach (var item in shoppingCart)
            {
                if(item.Count > item.ProductTaste.InStock)
                    return Result.Fail($"Niewystarczająca ilość produktu o nazwie: {item.ProductTaste.Product.Name}, dostępna ilość: {item.ProductTaste.InStock}");
                item.ProductTaste.InStock -= item.Count;
                var orderDetail = new OrderDetails()
                {
                    Count = item.Count,
                    OrderHeaderId = order.Id,
                    PricePerProduct = item.ProductTaste.Product.IsDiscount == true ? item.ProductTaste.Product.PromotionalPrice : item.ProductTaste.Product.RegularPrice,
                    ProductTasteId = item.ProductTaste.Id,  
                };
                order.OrderProductsTotal += orderDetail.PricePerProduct * item.Count;
                orderDetails.Add(orderDetail);
            }
            order.OrderShippingCost = order.OrderProductsTotal>=carrier.FreeShippingFromPrice?0:carrier.ShippingCost;
            var isDiscountCode = true;
            BRO.Domain.Entities.DiscountCode discountCode = null;
            if (command.DiscountCodeId != null)
            {
                discountCode = await _unitOfWork.DiscountCodeRepository.GetById((Guid)command.DiscountCodeId);
                if ((discountCode==null)||((order.OrderProductsTotal+order.OrderShippingCost)<discountCode.MinimumOrderPrice)||(discountCode.NumberOfUsesLeft <= 0)||(discountCode.CodeAvailabilityEnd < DateTimeOffset.Now))
                {
                    isDiscountCode = false;
                    return Result.Fail("Kod rabatowy nieprawidłowy, usuń aby kontynuować ");      
                }
            }
            else
                isDiscountCode = false;
            if (isDiscountCode)
            {
                order.OrderProductsTotal = order.OrderProductsTotal - order.OrderProductsTotal * ((double)discountCode.DiscountInPercent / (double)100);
                order.DiscountCodeId = discountCode.Id;
                order.DiscountInPercent = discountCode.DiscountInPercent;
                discountCode.NumberOfUsesLeft -= 1;
                
            }

            var productsTotal = command.OrderProductsTotalAfterDiscount != null ? command.OrderProductsTotalAfterDiscount : command.OrderProductsTotal;


            if ((order.OrderProductsTotal + order.OrderShippingCost) != productsTotal.Value + command.OrderShippingCost)
                return Result.Fail("Nie udało się złożyć zamówienia");

            Guid orderBillId;
            if (command.IsOrderBill)
            {
                orderBillId = Guid.NewGuid();
                var orderBill = new BRO.Domain.Entities.OrderBill();
                _mapper.Map(command.OrderBillCommand, orderBill);
                orderBill.Id = orderBillId;
                order.OrderBillId = orderBillId;
                await _unitOfWork.OrderBillRepository.AddAsync(orderBill);
            }
            order.ConcurrencyStamp = Guid.NewGuid().ToString();
          
            if(discountCode!=null)
                await _unitOfWork.DiscountCodeRepository.UpdateAsync(discountCode);
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.OrderDetailsRepository.AddRangeAsync(orderDetails);
            await _unitOfWork.ShoppingCartItemRepository.UpdateRangeAsync(shoppingCart); 
            await _unitOfWork.ShoppingCartItemRepository.RemoveRangeAsync(shoppingCart); 
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
