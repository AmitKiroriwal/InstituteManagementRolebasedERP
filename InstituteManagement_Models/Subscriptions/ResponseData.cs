using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class ResponseData
    {

        public string order_id { get; set; } = string.Empty;
        public string tracking_id { get; set; } = string.Empty;
        public string bank_ref_no { get; set; } = string.Empty;
        public string order_status { get; set; } = string.Empty;
        public string failure_message { get; set; } = string.Empty;
        public string payment_mode { get; set; } = string.Empty;
        public string card_name { get; set; } = string.Empty;
        public string status_code { get; set; } = string.Empty;
        public string status_message { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;

        public string amount { get; set; } = string.Empty;
        public string billing_name { get; set; } = string.Empty;
        public string billing_address { get; set; } = string.Empty;
        public string billing_city { get; set; } = string.Empty;
        public string billing_state { get; set; } = string.Empty;
        public string billing_zip { get; set; } = string.Empty;
        public string billing_country { get; set; } = string.Empty;
        public string billing_tel { get; set; } = string.Empty;

        public string billing_email { get; set; } = string.Empty;
        public string delivery_name { get; set; } = string.Empty;
        public string delivery_address { get; set; } = string.Empty;
        public string delivery_city { get; set; } = string.Empty;
        public string delivery_state { get; set; } = string.Empty;
        public string delivery_zip { get; set; } = string.Empty;
        public string delivery_country { get; set; } = string.Empty;
        public string delivery_tel { get; set; } = string.Empty;
        public string merchant_param1 { get; set; } = string.Empty;
        public string merchant_param2 { get; set; } = string.Empty;
        public string merchant_param3 { get; set; } = string.Empty;
        public string merchant_param4 { get; set; } = string.Empty;
        public string merchant_param5 { get; set; } = string.Empty;

        public string vault { get; set; } = string.Empty;
        public string offer_type { get; set; } = string.Empty;
        public string offer_code { get; set; } = string.Empty;
        public string discount_value { get; set; } = string.Empty;
        public string mer_amount { get; set; } = string.Empty;
        public string eci_value { get; set; } = string.Empty;
        public string retry { get; set; } = string.Empty;
        public string response_code { get; set; } = string.Empty;
        public string billing_notes { get; set; } = string.Empty;
        public string trans_date { get; set; } = string.Empty;
        public string bin_country { get; set; } = string.Empty;

    }
}
