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
    public class Client : IdentityUser<int>
    {

        [MaxLength(10)]
        public DateTime Registered { get; set; } = DateTime.Now;
        public UserCreditCard UserCreditCard { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int PersonId { get; set; }

    }
}
