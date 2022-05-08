﻿using System;
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
        
        [Range(0 , double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Price { get; set; }
        [Range(0, 6, ErrorMessage = "The value must be between 0 - 6")]
        public int Rating { get; set; }

        public Person Person { get; set; }
    }
}
