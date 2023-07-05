using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class Success
    {
        public string userid { get; set; }
        
        public string paidfee { get; set; }

        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
        public string status { get; set; }
    }
}
