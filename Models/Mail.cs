using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string To { get; set; }
       
        public string Subject { get; set; }

        public string Body { get; set; }


    }
}