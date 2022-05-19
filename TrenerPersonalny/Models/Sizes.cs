using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TrenerPersonalny.Models
{
    public class Sizes
    {   
        //[Key]
        [JsonProperty("id")]
        public int Id { get; set; }        

        public DateTime UpdateDate { get; set; } = DateTime.Now.Date;

        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [Required]
        public int PersonId { get; set; }

        public List<SizeDetails> SizeDetails { get; set; } = new();


        public void AddDetail(int excerciseTypeId, int sizeCm)
        {
            SizeDetails.Add(new SizeDetails
            {
                ExcerciseTypeId = excerciseTypeId,
                SizeCm = sizeCm
            });
        }

        public void UpdateDetail(int excerciseTypeId, int sizeCm)
        {
            var detail = SizeDetails
                .Where(o =>o.ExcerciseTypeId == excerciseTypeId)
                .FirstOrDefault();
            if (detail == null) return;
            detail.SizeCm = sizeCm;
        }

        public void RemoveDetail(int excerciseTypeId)
        {
            var detail = SizeDetails
                .FirstOrDefault(d => d.ExcerciseTypeId == excerciseTypeId);
            if (detail == null) return;
            SizeDetails.Remove(detail);
        }

        public void UpdateCm(int excerciseTypeId, Boolean add)
        {
            var detail = SizeDetails
                .Where(o => o.ExcerciseTypeId == excerciseTypeId)
                .FirstOrDefault();
            if (detail == null) return;
            if(add == true)
            {
                detail.SizeCm = detail.SizeCm + 1;
            } else
            {
                detail.SizeCm = detail.SizeCm - 1;

            }
            
        }

    }
}
