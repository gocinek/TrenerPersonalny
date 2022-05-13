using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrenerPersonalny.Models
{
    public class Excercises
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        //  public string Type { get; set; }

        [ForeignKey("ExcerciseTypeId")]
        public ExcerciseType ExcerciseType { get; set; }
        [Required]
        public int ExcerciseTypeId { get; set; }

        public string PublicId { get; set; }
    }
}