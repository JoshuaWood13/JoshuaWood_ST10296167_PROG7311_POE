using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JoshuaWood_ST10296167_PROG7311_POE.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string? FarmerCode { get; set; }

        public ICollection<Products> Products { get; set; }  // Navigation property to the Products model
    }
}
