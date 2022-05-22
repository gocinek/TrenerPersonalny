using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrenerPersonalny.Models
{
    public class Plans
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now.Date;

        [ForeignKey("TrainerId")]
        public Trainers Trainers { get; set; }
        public int TrainerId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public List<PlanDetails> PlanDetails { get; set; } = new();
    }
}
