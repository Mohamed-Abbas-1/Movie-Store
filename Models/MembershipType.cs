using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MovieStore.Models
{
    public class MembershipType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }

        public string Name { get; set; }

        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }

        public static readonly byte UnKnown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}
