using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class Trainers
    {
        public int Id { get; set; }
        [MaxLength(510)]
        public string Description { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }

        public Person Person { get; set; }
    }
}
