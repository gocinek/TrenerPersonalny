using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.Orders
{
    [Owned]
    public class TrainerPersonOrdered
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}
