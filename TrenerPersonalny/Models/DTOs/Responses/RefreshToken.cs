using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Responses
{
    public class RefreshToken
    {
        public int Id { get; set; }
        
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        //public ICollection<Client> User { get; set; }
        //  [ForeignKey("rolesId")]
        // [Required]
        // public UserRoles UserRoles { get; set; }
        // public int rolesId { get; set; }
    }
}
