using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace demo_project.BLL
{
    class BLL_transaction
    {
        public int inv_id { get; set; }
        public float grand_total { get; set; }
        public DateTime date { get; set; }
        public DataTable transactionDetails { get; set; }
    }
}
