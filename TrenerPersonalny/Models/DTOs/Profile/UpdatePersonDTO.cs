using Microsoft.AspNetCore.Http;

namespace TrenerPersonalny.Models.DTOs.Profile
{
    public class UpdatePersonDTO
    {
       // [JsonProperty("id")]
       // public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public IFormFile File { get; set; }
    }
}
