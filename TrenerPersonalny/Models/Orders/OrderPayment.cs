using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.Orders
{
    public class OrderPayment
    {
        public int Id { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public string BuyerId { get; set; }
    }
}
