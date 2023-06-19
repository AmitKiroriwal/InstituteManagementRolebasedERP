using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models
{
    public class ApplicationUser:IdentityUser
    {

        [Column(TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        //  
        [Column(TypeName = "nvarchar(200)")]
        public string? InstituteName { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNo_1 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? ContactNo_2 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        
        public string? State { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]

        public string? District { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string? City { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string? Pincode { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? StateCode { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? GSTNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? PanNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? UANNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? BankAccName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? BankAccNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? IfscCode { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Branch { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Logo { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Signature { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? TermsAndCondition { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ParentId { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? MapPath { get; set; }
       
        public DateTime? AddedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser? AppUserAddedBy { get; set; } // Self Reference
        [ForeignKey("AppUserAddedBy")]
        public string? AddedBy { get; set; }
        public ApplicationUser? AppUserUpdatedBy { get; set; } // Self Reference
        [ForeignKey("AppUserUpdatedBy")]
        public string? UpdatedBy { get; set; }
        public string? UserPassword { get; set; }
        
        [NotMapped]
        public string? RoleName { get; set; }
        public Status Status { get; set; }
    }
}
