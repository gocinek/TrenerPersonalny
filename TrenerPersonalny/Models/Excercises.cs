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
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

      //  public string Type { get; set; }

        [ForeignKey("excerciseTypeId")]
        [Required]
        public ExcerciseType Type { get; set; }
        public int excerciseTypeId { get; set; }


    }
}
