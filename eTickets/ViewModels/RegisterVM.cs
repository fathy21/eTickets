using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Full Name Is Required")]
        [Display(Name ="Full Name")]
       public string FullName { get; set; }

        [Required(ErrorMessage ="the Email Address Is Required")]
        [Display(Name = "Email Address")]
        public string EmailAddress {get; set; }

        [Required(ErrorMessage = "The Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
