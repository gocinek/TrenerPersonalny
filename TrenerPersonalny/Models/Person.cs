using Newtonsoft.Json;
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
        [JsonProperty("id")]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ProfileImg { get; set; }
        public string PublicId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainers Trainers { get; set; }
        public int? TrainerId { get; set; }

        public User Client { get; set; }

    }
}
