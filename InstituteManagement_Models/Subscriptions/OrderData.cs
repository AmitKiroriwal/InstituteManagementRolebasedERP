using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class OrderData
    {
        public int Id { get; set; }
        public string tid { get; set; } = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();
        public string Merchant_id { get; set; }
        public string Order_id { get; set; }
        public int planId { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Redirect_url { get; set; }
        public string Cancel_url { get; set; }
        public string Billing_name { get; set; }
        public string Billing_address { get; set; }

        public string Billing_city { get; set; }
        public string Billing_state { get; set; }
        public string Billing_zip { get; set; }
        public string Billing_country { get; set; }
        public string Billing_tel { get; set; }
        public string Billing_email { get; set; }
        public string AppUserId { get; set; }
    }
}
