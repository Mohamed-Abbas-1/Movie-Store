using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MovieStore.Models;
namespace MovieStore.Controllers
{
   
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext _Context;
        public HomeController()
        {
            _Context = new ApplicationDbContext();
        }




        // Cashing
        //[OutputCache(Duration = 50)] //// saving as cash for 50s then saving again....
        //[OutputCache(Duration =50, Location = OutputCacheLocation.Server, VaryByParam = "gener")]  // specify cashing for specific one parameter...
        //[OutputCache(Duration = 50, Location = OutputCacheLocation.Server, VaryByParam = "*")]  // specify cashing for specific multiple parameter...
        //[OutputCache(Duration = 0,VaryByParam ="*",NoStore =true)] //// disaple cashing...
        public ActionResult Index()
        {
            //// Cashing some Data that doesn't changes .... ////// Don't use it if you don't have to....
            //if (MemoryCache.Default["Geners"] == null)
            //{
            //    MemoryCache.Default["Geners"] = _Context.Genres.ToList();
            //}
            //var geners = MemoryCache.Default["Geners"] as IEnumerable<Genre>;

            return View();
        }

        public ActionResult About()
        {
            throw new Exception();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}