using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using MovieStore.ViewModels;
using Microsoft.AspNet.Identity;
namespace MovieStore.Controllers
{
    public class RentalsController : Controller
    {
        ApplicationDbContext _Context;
        public RentalsController()
        {
            _Context = new ApplicationDbContext();
        }

        // GET: Rentals
        public ActionResult New()
        {
            return View();
        }

        public ActionResult RentedMovies()
        {
            var RentedMovies = _Context.Rentals.Include(r => r.Movie).Include(r => r.Customer).Include(c=>c.Customer.ApplicationUser).Where(r => r.DateReturned == null).ToList();

            if (User.IsInRole(RoleName.CanManageMovies))
                return View(RentedMovies);


            var userId = User.Identity.GetUserId();

            var customerIds = from customer in _Context.Customers
                             where customer.ApplicationUserId == userId
                             select customer.Id;
            
                var RentedMoviesForUsers = _Context.Rentals.Include(r=>r.Movie).Include(r=>r.Customer).Where(r=>customerIds.Contains(r.CustomerId)&& r.DateReturned == null).ToList();

            return View("RentedMoviesForUsers", RentedMoviesForUsers);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Requests()
        {
            var RentedMovies = _Context.Rentals.Include(r => r.Movie).Include(r => r.Customer).Include(c => c.Customer.ApplicationUser).Where(r => r.DateReturned == null).ToList();
            return View(RentedMovies);
        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult AllRentedList()
        {
            var rentedMovies = _Context.Rentals.Include(r => r.Movie).Include(r => r.Customer).Include(r=>r.Customer.ApplicationUser).ToList();
               
            return View(rentedMovies);
        }

       public void ChangeDateReturned(int id , DateTime dateReturned)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);

            rentedMovie.DateReturned = dateReturned;
            _Context.SaveChanges();
        }

        public void AskedReturn(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            rentedMovie.AskedReturn = true;
            rentedMovie.AskedReturnDate = DateTime.Now;
            _Context.SaveChanges();
        }
        
        public void AcceptReturnRequest(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            rentedMovie.DateReturned = DateTime.Now;
            _Context.SaveChanges();
        }

        public void CancelAskedReturnRequest(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            rentedMovie.AskedReturn = null;
            _Context.SaveChanges();
        }

        public void AcceptRentRequest(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            rentedMovie.DateRented = DateTime.Now;
            _Context.SaveChanges();
        }

        public void CancelRentRequest(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            rentedMovie.AskedRented = null;
            _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var rentedMovie = _Context.Rentals.Single(r => r.Id == id);
            _Context.Rentals.Remove(rentedMovie);
            _Context.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}