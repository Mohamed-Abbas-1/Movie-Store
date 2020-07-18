using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieStore.Dtos;
using MovieStore.Models;
using AutoMapper;
namespace MovieStore.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _Context;
        public NewRentalsController()
        {
            _Context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto newRental)
        {
            if (newRental == null)
                return BadRequest("No movie Ids have been given.");
                var customer = _Context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("Invalid Customer ID.");
                var movies = _Context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("one or more MovieIds are invalide.");

           
            foreach (var movie in movies)
            {
                var numberAvailable = movie.NumberInStock - _Context.Rentals.Where(r=>r.MovieId == movie.Id).Where(r => r.DateReturned == null && r.AskedRented != null).Count();
                if (numberAvailable <= 0)
                    return BadRequest("Movie is not Available.");

                if (User.IsInRole(RoleName.CanManageMovies))
                {
                    var Rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        AskedRented = true,
                        AskedRentedDate = DateTime.Now,
                        DateRented = DateTime.Now
                        
                    };
                    _Context.Rentals.Add(Rental);
                }
                else
                {
                    var Rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        AskedRented = true,
                        AskedRentedDate = DateTime.Now
                    };
                    _Context.Rentals.Add(Rental);
                }
                
            }
            _Context.SaveChanges();
            return Ok("Rentals are successfully created.");
        }
    }
}
