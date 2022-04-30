using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models.DTOs.Requests;

namespace TrenerPersonalny.Models.DTOs.Responses
{
    public class UserRegistrationDto : UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Person Person { get; set; }
    }
}
