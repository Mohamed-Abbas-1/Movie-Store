using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStore.Models;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace MovieStore.Models
{
    public class Roles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}