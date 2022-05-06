using System;
using System.Collections.Generic;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Models.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }  //login usera
        public DateTime OrderDate { get; set; }
        public List<OrderTrainerDto> OrderTrainer { get; set; }
        public string OrderStatus { get; set; }
        public DateTime Expired { get; set; }
        public int Summary { get; set; }
        public string PaymentIntentId { get; set; }
        public UsedCreditCard usedreditCard { get; set; }
    }
}