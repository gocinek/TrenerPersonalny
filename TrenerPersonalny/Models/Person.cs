using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class Person
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(5)]
        public string Gender { get; set; }
        [MaxLength(255)]
        public string ProfileImg { get; set; }
        [MaxLength(25)]
        public string Language { get; set; }
        [MaxLength(25)]
        public string Nationality { get; set; }
        [MaxLength(9)]
        public int PhoneNumber { get; set; }

    }
}
