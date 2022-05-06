using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models.DTOs.Orders;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<Order> query)
        {
            return query
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    BuyerId = order.BuyerId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus.ToString(),
                    Expired = order.Expired,
                    Summary = order.Summary,
                    PaymentIntentId = order.PaymentIntentId,
                    OrderTrainer = order.OrderTrainer
                    .Select(item => new OrderTrainerDto
                    {
                        TrainerId = item.TrainerOrdered.TrainerId,
                        Name = item.TrainerOrdered.Name,
                        PictureUrl = item.TrainerOrdered.PictureUrl,
                        Price = item.Price
                    })
                    .ToList()                    
                }).AsNoTracking();
        }
    }
}
