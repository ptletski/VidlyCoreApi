using System;
using System.ComponentModel.DataAnnotations;

namespace VidlyCoreApp.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MembershipTypeValidation]
        public byte MembershipTypeId { get; set; }

        [MembershipAgeValidation]
        public DateTime? BirthDate { get; set; }
    }
}
