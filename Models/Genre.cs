using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MovieStore.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(20,ErrorMessage ="Genre Name can't be more than 20 chars.")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}