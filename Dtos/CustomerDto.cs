using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MovieStore.Models;
namespace MovieStore.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please insert the customer's Name.")]
        [StringLength(50, ErrorMessage = "Customer Name can't be more than 50 chars.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please insert the customer's Phone or address to easily recive your orders.")]
        [Display(Name = "Address/Phone")]
        public string AddressOrPhone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipTypeDto MembershipType { get; set; }

        [Required(ErrorMessage = "Please insert the membership type.")]
        public byte MembershipTypeId { get; set; }
    }
}