using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models.Orders;

namespace TrenerPersonalny.Models.DTOs.Orders
{
    public class CreateOrderDto
    {
        public bool SaveCreditCard { get; set; }
        public UsedCreditCard UsedCreditCard { get; set; }
    }
}
