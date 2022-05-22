using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class User : IdentityUser<int>
    {
       

        [MaxLength(10)]
        public DateTime Registered { get; set; } = DateTime.Now.Date;
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [JsonProperty("id")]
        public int PersonId { get; set; }

    }
}
