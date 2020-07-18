﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
namespace MovieStore.ViewModels
{
    
    public class UsersViewModel
    {
        public IEnumerable<string> UserId { get; set; }
        public IEnumerable<string> UserName { get; set; }
        public IEnumerable<string> Role { get; set; }
    }
}