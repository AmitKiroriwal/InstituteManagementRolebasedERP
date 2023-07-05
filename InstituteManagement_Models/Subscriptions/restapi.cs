using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstituteManagement_Models.Subscriptions
{
    public class restapi
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<int> data { get; set; }

    }
}
