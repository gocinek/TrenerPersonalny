﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class ExcerciseType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}