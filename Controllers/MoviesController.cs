using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using MovieStore.ViewModels;

namespace MovieStore.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Get:Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

      
        public ActionResult Details(int id)
        {
            

            var movie = db.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = db.Genres.ToList(),

            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = db.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }


            if(movie.Id == 0)
            {
                movie.NumberAvailable = movie.NumberInStock;
                db.Movies.Add(movie);
            }
            else
            {
                var specifyMovie = db.Movies.Single(m => m.Id == movie.Id);

                specifyMovie.Name = movie.Name;
                specifyMovie.ReleaseDate = movie.ReleaseDate;
                specifyMovie.GenreId = movie.GenreId;
                specifyMovie.NumberInStock = movie.NumberInStock;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index","Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = db.Movies.Include(m=>m.Genre).SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }
            
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = db.Genres.ToList(),
                Rentals = db.Rentals.Where(r=>r.MovieId == id)
            };
            return View("MovieForm", viewModel);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int id)
        {
            var movie = db.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null) return HttpNotFound();

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }










        ///// <summary>
        ///// Testing
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>

        //// GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie {Name = "Shrek!" };
        //    var customers = new List<Customers>
        //    {
        //       new Customers {Name = "Customer 1" },
        //       new Customers {Name = "Customer 2" }
        //    };
        //    var viewModel = new RandomMovieViewModel {
        //        Movie = movie,
        //        Customers = customers 
        //    };
        //    return View(viewModel);
        //}


        //// Get: Movies/Edit
        //public ActionResult Edit(int id)
        //{
        //    return Content("Id ="+ id);
        //}
        //// Get: Movies
        //public ActionResult Index( int? pageIndex , string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if(string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(string.Format("pageIndex = {0} & sortBy = {1}.",pageIndex , sortBy)); // using string.format to use ({0},var).
        //}
        //[Route("movies/released/{year:regex(\\d{4}):range(1900,2500)}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleasedDate(short year , byte month)
        //{
        //    return Content(string.Format("{0}/{1}", year, month));
        //}
    }
}