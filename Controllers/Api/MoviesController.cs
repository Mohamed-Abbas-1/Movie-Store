using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MovieStore.Dtos;
using MovieStore.Models;
namespace MovieStore.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            
            var moviequery = db.Movies.Include(m => m.Genre).Where(m => m.NumberInStock - m.Rentals.Where(r=>r.DateReturned == null && r.AskedRented != null).Count() > 0);
            if (!string.IsNullOrWhiteSpace(query))
                moviequery = moviequery.Where(m => m.Name.Contains(query));
                
             var movieDto = moviequery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDto);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = db.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }


        // POST /api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            db.Movies.Add(movie);
            db.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        // PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movieInDb = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();
            Mapper.Map(movieDto, movieInDb);
            db.SaveChanges();
            return Ok();
        }

        // DELETE  /api/movies/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovies(int id)
        {
            var movie = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            db.Movies.Remove(movie);
            db.SaveChanges();
            return Ok();
        }
    }
}
