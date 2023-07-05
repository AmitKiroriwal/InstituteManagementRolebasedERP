using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get;set; }
        public Plans Plans { get; set; }

        [ForeignKey("Plans")]
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; } // Start date of the subscription
        public DateTime EndDate { get; set; } // End date of the subscription
        public bool IsActive { get; set; } // Indicates if the subscription is active or expired
        public decimal AmountPaid { get; set; } // Amount paid for the subscription
        public DateTime PaymentDate { get; set; } // Date of payment
        public bool IsPaymentComplete { get; set; } // Indicates if the payment is complete
        public DateTime CreationDate { get; set; } // Date and time when the subscription was created
    }
}
