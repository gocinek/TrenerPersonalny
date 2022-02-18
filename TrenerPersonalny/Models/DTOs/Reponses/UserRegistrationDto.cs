﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Reponses
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Pasword { get; set; }
        [Required]
        public int rolesId { get; set; }
    }
}
