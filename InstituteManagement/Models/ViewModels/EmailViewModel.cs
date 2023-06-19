using System.ComponentModel.DataAnnotations;

namespace InstituteManagement.Models.ViewModels
{
    public class EmailViewModel
    {
        public string Email { get; set; }   
        public bool IsEmailConfirmed { get; set; }
        public string StatusMessage { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }
    }
}
