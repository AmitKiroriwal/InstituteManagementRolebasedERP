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
    public class District
    {
        [Key]
        public int DistrictId { get; set; }


        [Required]

        [Column(TypeName = "nvarchar(50)")]
        public string? DistrictName { get; set; }


        public State? State { get; set; }
        [ForeignKey("State")]
        public int? StateID { get; set; }


    }
}
