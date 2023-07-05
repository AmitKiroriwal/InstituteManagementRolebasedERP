using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class Payment
    {
        public int Id { get; set; }
        public int srNo { get; set; }
        public string status { get; set; }
        public string amountPaid { get; set; }
        public string razorpay_Payment_Id { get; set; }
        public string razorpay_Order_Id { get; set; }
        public string razorpay_Signature { get; set; }

        public string createdat { get; set; }
        public string authat { get; set; }
        public string authcode { get; set; }
        public string ctype { get; set; }
        public string desc { get; set; }
        public string method { get; set; }
        public string capat { get; set; }
        public string currency { get; set; }
        public string AppUserId { get; set; }
        public int PlanId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
