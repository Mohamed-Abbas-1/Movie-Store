using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MovieStore.ViewModels;
using Microsoft.AspNet.Identity;
namespace MovieStore.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }



        // Get: Customers/Details
        public ActionResult Details(int id)
        {
            //var customers = GetCustomers();
            //var customer = from thisCustomer in customers
            //               where thisCustomer.Id == id
            //               select thisCustomer;
            var customer = db.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = db.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            bool IsNameExist = db.Customers.Any(c => c.Name == customer.Name && c.Id != customer.Id);
            if (IsNameExist == true)
            {
                ModelState.AddModelError("customer.Name", "Customer already exists");
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = db.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
           
            if (customer.Id == 0)
            {
                customer.ApplicationUserId = User.Identity.GetUserId();
                db.Customers.Add(customer);
            }
            else
            {
                var specifyCustomer = db.Customers.Single(c => c.Id == customer.Id);
                specifyCustomer.Name = customer.Name;
                specifyCustomer.AddressOrPhone = customer.AddressOrPhone;
                specifyCustomer.Birthdate = customer.Birthdate;
                specifyCustomer.MembershipTypeId = customer.MembershipTypeId;
                specifyCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Customers");

        }
        public ActionResult Edit(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = db.MembershipTypes.ToList(),
                Rentals = db.Rentals.ToList()
            };
            return View("CustomerForm", viewModel);
        }
        public ActionResult Delete(int id)
        {
            var cutomerRented = db.Rentals.Where(r => r.CustomerId == id && r.DateReturned == null && r.DateRented != null).Count();
            if (cutomerRented == 0)
            {
                var customer = db.Customers.SingleOrDefault(c => c.Id == id);

                db.Customers.Remove(customer);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
            

        }

        public ActionResult RemoveAll()
        {
            db.Customers.RemoveRange(db.Customers);
            db.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }



        //public ActionResult IsCustomerAvailble(Customer customer, int? id)
        //{
                
        //    try
        //        {
        //            var tag = db.Customers.Single(m => m.Name == customer.Name && m.Id != id);
        //            return Json(false, JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception)
        //        {
        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
            
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

