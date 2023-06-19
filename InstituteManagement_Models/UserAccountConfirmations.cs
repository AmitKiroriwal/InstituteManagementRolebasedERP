using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models
{
    public class UserAccountConfirmations
    {
        public int id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string? UserAccountId { get; set; }
        
        public bool EmailConfirmation { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
