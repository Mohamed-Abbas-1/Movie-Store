using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MovieStore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please insert the customer's Name.")]
        [StringLength(50,ErrorMessage ="Customer Name can't be more than 50 chars.")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate{ get; set; }

        [Required(ErrorMessage = "Please insert the customer's Phone or address to easily recive your orders.")]
        [Display(Name = "Address/Phone")]
        public string AddressOrPhone { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }
        [Required(ErrorMessage ="Please insert the membership type.")]
        [Display(Name = "Membership Type")]
        
        public byte MembershipTypeId { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}