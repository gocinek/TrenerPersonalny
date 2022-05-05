using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Orders
{
    public class OrderTrainerDto
    {
        public int TrainerId { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }
        public int Price { get; set; }
    }
}
