namespace RealEstate.ViewModels.Identity
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class AccountViewModels
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool IsRemember { get; set; }
    }

    public class ChangePasswordViewModels
    {
        [Required(ErrorMessage = "This fied is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "This fied is required")]
        [MinLength(6, ErrorMessage = "Minimum length is 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This fied is required")]
        [Compare("Password", ErrorMessage = "Invalid confirm password")]
        public string ConfirmPassword { get; set; }
    }
}