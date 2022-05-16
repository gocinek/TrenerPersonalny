using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrenerPersonalny.Models
{
    public class SizeDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        public int SizeCm { get; set; } //cm

        [ForeignKey("ExcerciseTypeId")]
        public ExcerciseType ExcerciseType { get; set; }
        [Required]
        public int ExcerciseTypeId { get; set; }

        [ForeignKey("SizesId")]
        public Sizes Sizes { get; set; }
        [Required]
        public int SizesId { get; set; }

    }
}
