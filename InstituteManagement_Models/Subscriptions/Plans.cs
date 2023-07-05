using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class Plans
    {
        [Key]
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public string PlanDescription { get; set; }
        public string Duration { get;set; }
        public decimal Price { get; set; }
        public string Features { get; set; }
        public int Discount { get; set; }
        
        public bool IsActive { get; set; } // Indicates if the plan is active or inactive
        public DateTime CreationDate { get; set; } // Date and time when the plan was created
    }
}
