using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class SubscriptionData
    {
        
        public int Id { get; set; }
        public string Amount { get; set; }

        public string Status { get; set; }
        public string Razorpay_payment_id { get; set; }
        public string Razorpay_order_id { get; set; }
        public string Razorpay_signature { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string AppUserId { get; set; }
        public int SessionId { get; set; }
        
        public Plans Plans { get; set; }
        [ForeignKey(nameof(Plans))]
        public int OrderId { get; set; }
        public System.DateTime Date { get; set; } = System.DateTime.Now;
        public string UserName { get; set; }
        public decimal WhatDoYouWantToPay { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEMail { get; set; }
        public string ClientAddress { get; set; }
        public string Discount { get; set; }
        public string Deposited { get; set; }
        public string Balance { get; set; }
        
        
        public string PlanName { get; set; }
        public string Price { get; set; }
    }
}
