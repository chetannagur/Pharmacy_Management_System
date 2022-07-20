using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_project.BLL
{
    class BLL_medicine
    {
        public int med_id { get; set; }
        public string med_name { get; set; }
        public string med_manifacturer { get; set; }
        public DateTime med_expdate { get; set; }
        public float med_buyingprice { get; set; }
        public float med_sellingprice { get; set; }
        public int med_quantity { get; set; }
    }
}
