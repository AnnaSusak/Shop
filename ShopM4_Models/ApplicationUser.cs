using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopM4_Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string Street { get; set; }
        [NotMapped]
        public string House { get; set; }
        [NotMapped]
        public string Apartment { get; set; }
        [NotMapped]
        public string PostalCode { get; set; }
    }
}

