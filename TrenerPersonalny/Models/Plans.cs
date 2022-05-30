using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrenerPersonalny.Models
{
    public class Plans
    {
        [JsonProperty("id")]
        public int Id { get; set; }       

        [ForeignKey("TrainerId")]
        public Trainers Trainers { get; set; }
        public int TrainerId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int PersonId { get; set; }

        public List<PlanDetails> PlanDetails { get; set; } = new();
        public DateTime UpdatedDate { get; set; } = DateTime.Now.Date;

        public void AddDetail(int excerciseId, int repeats, int manyInWeek)
        {
            var detail = PlanDetails
                .Where(o => o.ExcerciseId == excerciseId)
                .FirstOrDefault();
            if (detail == null)
            {
                PlanDetails.Add(new PlanDetails
                {
                    ExcerciseId = excerciseId,
                    Repeats = repeats,
                    ManyInWeek = manyInWeek
                });
            } else
            {
                detail.ManyInWeek = manyInWeek;
                detail.Repeats = repeats;
            }
                        
          
        }

        public void RemoveDetail(int excerciseId)
        {
            var plan = PlanDetails
                .FirstOrDefault(d => d.ExcerciseId == excerciseId);
            if (plan == null) return;
            PlanDetails.Remove(plan);
        }
    }
}
