using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class UserRole
    {
        [Key]
        public int rolesId { get; set; }

        [MaxLength(50)]
        public string role { get; set; }

        public ICollection<Client> Client { get; set; }

    }
}
