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
    [Table("tblRegion", Schema = "dbo")]
    public class Region
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? City { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? State { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? District { get; set; }
    }
}
