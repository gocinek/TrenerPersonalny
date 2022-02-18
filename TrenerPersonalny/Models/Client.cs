﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class Client : IdentityUser
    {   
        
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      //  [Key]
      //  public int userId { get; set; }

      //  public string Id { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(5)]
        public string Gender { get; set; }
        [MaxLength(10)]
        public string Registered { get; set; }
        [MaxLength(255)]
        public string ProfileImg { get; set; }
        [MaxLength(25)]
        public string Language { get; set; }
        [MaxLength(25)]
        public string Nationality { get; set; }

        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        [Required]
        public string PasswordSalt { get; set; }

        [ForeignKey("rolesId")]
        [Required]
        public UserRoles UserRoles { get; set; }
        public int rolesId { get; set; }


        //public string NormalizedEmail { get; set; }

    }
}
