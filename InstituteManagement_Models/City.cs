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
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? CityName { get; set; }
        public District? District { get; set; }
        [ForeignKey("District")]
        public int? DistrictId { get; set; }
    }
}
