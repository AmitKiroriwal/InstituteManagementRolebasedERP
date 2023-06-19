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
    public class State
    {
        [Key]
        public int StateID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? StateName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? StateCode { get; set; }
    }
}
