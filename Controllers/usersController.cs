using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using MovieStore.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MovieStore.Controllers
{
    public class UsersController : Controller
    {

        // GET: users
        ApplicationDbContext _context ;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult AllUsers()
        {
            var users = _context.Users.Include(u => u.Roles).ToList();

            return View(users);
        }

        public  ActionResult SetAdmin(string id)
        {
            var roleId = _context.Roles.First(r => r.Name == RoleName.CanManageMovies).Name;

            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(id, roleId);


            return RedirectToAction("AllUsers");
        }

        public ActionResult SetUser(string id)
        {
            var roleId = _context.Roles.First(r => r.Name == RoleName.CanManageMovies).Name;


            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.RemoveFromRole(id, roleId);

            return RedirectToAction("AllUsers");
        }

        public void Delete(string id)
        {
            var user = _context.Users.Single(u => u.Id == id);
            var userRentedCount = _context.Rentals.Where(r => r.Customer.ApplicationUserId == id && r.DateReturned == null && r.DateRented != null).Count();

            if (userRentedCount == 0)
            {
                var userRented = _context.Rentals.Where(r => r.Customer.ApplicationUserId == id);
                var userCustomers = _context.Customers.Where(r => r.ApplicationUserId == id);
                _context.Rentals.RemoveRange(userRented);
                _context.Customers.RemoveRange(userCustomers);
                _context.SaveChanges();
            }
                _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}