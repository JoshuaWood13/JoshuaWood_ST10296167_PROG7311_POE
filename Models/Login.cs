using System.ComponentModel.DataAnnotations;

namespace JoshuaWood_ST10296167_PROG7311_POE.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter a email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//