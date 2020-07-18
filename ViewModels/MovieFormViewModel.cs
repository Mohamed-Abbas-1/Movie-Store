using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStore.Models;
namespace MovieStore.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Rental> Rentals { get; set; }
    }
}