using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TrenerPersonalny.Models.DTOs.Profile
{
    public class UpdatePersonDTO
    {
       // [JsonProperty("id")]
        public int Id { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
        public IFormFile File { get; set; }
        public int PhoneNumber { get; set; }
    }
}
