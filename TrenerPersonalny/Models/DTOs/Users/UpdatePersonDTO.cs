using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrenerPersonalny.Models.DTOs.Users
{
    public class UpdatePersonDTO
    {
        
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Gender { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public int? TrainerId { get; set; }
        public IFormFile File { get; set; }

    }
}
