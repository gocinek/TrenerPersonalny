using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Orders
{
    public class OrderPaymentDto
    {
        public int Id { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public string BuyerId { get; set; }
    }
}
