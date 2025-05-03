using System.ComponentModel.DataAnnotations;

namespace JoshuaWood_ST10296167_PROG7311_POE.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a email")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string? FarmerCode { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
