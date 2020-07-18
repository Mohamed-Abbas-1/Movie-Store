using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MovieStore.Models
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required (ErrorMessage ="Customer cannot be empty.")]
        public int CustomerId { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required(ErrorMessage = "Movie cannot be empty.")]
        public int MovieId { get; set; }
        public bool? AskedRented { get; set; }
        public DateTime? AskedRentedDate { get; set; }
        [Display(Name ="Date Rented")]
        public DateTime? DateRented { get; set; }
        [Display(Name = "Date Returned")]
        public DateTime? DateReturned { get; set; }
        public bool? AskedReturn { get; set; }
        public DateTime? AskedReturnDate { get; set; }
    }
}