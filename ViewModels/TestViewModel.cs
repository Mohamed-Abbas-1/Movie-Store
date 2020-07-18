using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStore.Models;
namespace MovieStore.ViewModels
{
    public class TestViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}