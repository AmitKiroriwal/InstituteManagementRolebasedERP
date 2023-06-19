using System.ComponentModel.DataAnnotations;

namespace InstituteManagement.Models.ViewModels
{
    public class RegisterAdminViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string PhoneNumber { get; set; }
        public string ContactNo_1 { get; set; }
        public string UserRole { get; set; } = "Admin";
        public string? TermsAndCondition { get; set; }
    }
}
