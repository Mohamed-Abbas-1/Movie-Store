using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieStore.Models;
using MovieStore.Dtos;
using AutoMapper;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
namespace MovieStore.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET /api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customerQuery = db.Customers.Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));
            if (!User.IsInRole(RoleName.CanManageMovies))
            {
                var userId = User.Identity.GetUserId();
                customerQuery = customerQuery.Where(c => c.ApplicationUserId == userId);
            }


            var customerDto =  customerQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDto);
        }

        // GET  /api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = db.Customers.SingleOrDefault(c=> c.Id == id);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        // POST  /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            db.Customers.Add(customer);
            db.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
        }

        // PUT  /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto) // You can use void or Customer , all will works well.
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customerInDb = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();
            Mapper.Map(customerDto, customerInDb);

            db.SaveChanges();
            return Ok();
        }
        // DELETE  /api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {

            var customer = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            var cutomerRented = db.Rentals.Where(r => r.CustomerId == id && r.DateReturned == null && r.DateRented != null).Count();
            if (cutomerRented == 0)
            {

                db.Customers.Remove(customer);
                db.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest("test");
            }

            
        }
    }
}
