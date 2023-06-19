using System.ComponentModel.DataAnnotations;

namespace InstituteManagement.Models.ViewModels
{
    public class RegisterViewModel
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

       
        public string PhoneNumber { get; set; }
        // 
        public string? InstituteName { get; set; }

        
        public string ContactNo_1 { get; set; }
        public string? ContactNo_2 { get; set; }
        
        public string? State { get; set; }

        

        public string? District { get; set; }
        
        public string? City { get; set; }
        
        public string? Pincode { get; set; }

        public string? Address { get; set; }

        public string? StateCode { get; set; }
        public string? GSTNo { get; set; }
        public string? PanNo { get; set; }
        public string? UANNo { get; set; }
        public string? BankAccName { get; set; }
        public string? BankAccNo { get; set; }
        public string? IfscCode { get; set; }
        public string? Branch { get; set; }
        public IFormFile? Logo { get; set; }
        public IFormFile? Signature { get; set; }
        public string? TermsAndCondition { get; set; }
        public string? ParentId { get; set; }
        public string? MapPath { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? AddedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
} 
