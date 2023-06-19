using System.ComponentModel.DataAnnotations;

namespace InstituteManagement.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
