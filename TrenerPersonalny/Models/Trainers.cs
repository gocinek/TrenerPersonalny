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
        [MaxLength(255)]
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public long Price { get; set; }
        [MaxLength(1)]
        public int Rating { get; set; }

        [ForeignKey("personId")]
        [Required]
        public Person person { get; set; }
        public int personId { get; set; }
    }
}
