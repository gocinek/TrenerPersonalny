using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models.DTOs.Users
{
    public class UpdatePersonDTO
    {

        public int Id { get; set; }
        
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public IFormFile File { get; set; }

        public int PhoneNumber { get; set; }

        public int? TrainerId { get; set; }
    }
}
