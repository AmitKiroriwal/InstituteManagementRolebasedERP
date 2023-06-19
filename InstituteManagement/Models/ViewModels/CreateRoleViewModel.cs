using System.ComponentModel.DataAnnotations;

namespace InstituteManagement.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
