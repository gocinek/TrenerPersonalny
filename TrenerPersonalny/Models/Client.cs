using Microsoft.AspNetCore.Identity;
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

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        [MaxLength(10)]
        public string Registered { get; set; }


        [ForeignKey("rolesId")]
        [Required]
        public UserRoles UserRoles { get; set; }
        public int rolesId { get; set; }

        //public string NormalizedEmail { get; set; }

    }
}
