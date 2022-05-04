using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.Orders
{
    public class OrderTrainer
    {
        public int Id { get; set; }

        public TrainerPersonOrdered TrainerOrdered { get; set; }

        public int Price { get; set; }

    }
}
