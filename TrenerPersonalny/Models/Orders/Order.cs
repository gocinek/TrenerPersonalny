using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }  //login usera
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderTrainer OrderTrainer { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    }
}
