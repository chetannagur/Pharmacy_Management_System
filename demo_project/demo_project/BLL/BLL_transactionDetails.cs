using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_project.BLL
{
    class BLL_transactionDetails
    {
        public int id { get; set;}
        public int inv_id { get; set; }
        public int med_id{get; set;}
        public string med_name { get; set; }
        public int quantity { get; set; }
        public float selling_price { get; set; }
        public float total_price { get; set; }
        public DateTime date { get; set; }
    }
}
