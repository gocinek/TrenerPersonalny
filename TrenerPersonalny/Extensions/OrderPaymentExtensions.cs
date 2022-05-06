
using TrenerPersonalny.Models.DTOs.Orders;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Extensions
{
    public static class OrderPaymentExtensions
    {
        public static OrderPaymentDto MapOrderPaymentToDto(this OrderPayment order)
        {
            return new OrderPaymentDto
            {
                Id = order.Id,
                PaymentIntentId = order.PaymentIntentId,
                ClientSecret = order.ClientSecret,
                BuyerId = order.BuyerId
            };          
        }
    }
}
