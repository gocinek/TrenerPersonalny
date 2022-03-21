using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class Trainers
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string Price { get; set; }
        [MaxLength(1)]
        public int Rating { get; set; }

        [ForeignKey('')]
    }
}
