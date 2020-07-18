using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieStore.Models;
using MovieStore.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieStore.Controllers.Api
{
    public class UsersController : ApiController
    {
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetUsers(string query)
        {
            var _context = new IdentityDbContext();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var result = from users in _context.Users
                           where users.UserName.Contains(query)
                           select new
                           {
                               UserId = users.Id,
                               UserName = users.UserName
                           };
                return Ok(result);
            }
                return Ok();
            
        }
    }
}
