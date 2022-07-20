using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace demo_project.UI
{
    public partial class bill_form : Form
    {
        public bill_form(int a)
        {
            InitializeComponent();
            invoice_id = a;
        }
        int invoice_id;
        DAL.Dal_Transaction_details tdd = new DAL.Dal_Transaction_details();
        DAL.Dal_transaction tddal = new DAL.Dal_transaction();

        private void bill_form_Load(object sender, EventArgs e)
        {
            
            List<BLL.BLL_transactionDetails> list = new List<BLL.BLL_transactionDetails>();
            DataSet ds = tdd.Invoice_product(invoice_id);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                list.Add(new BLL.BLL_transactionDetails
                {
                 
                    med_name = dr["med_name"].ToString(),
                    selling_price = float.Parse(dr["med_selling_price"].ToString()),
                    quantity = int.Parse(dr["med_quantity"].ToString()),
                    total_price = float.Parse(dr["med_total_price"].ToString())

                });
            }
            DataSet ds2 = tddal.Invoice_product(invoice_id);
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {

                invoice1.SetDataSource(list);
                invoice1.SetParameterValue("p_inv_no", dr["inv_id"].ToString());
                invoice1.SetParameterValue("p_grand_total", dr["grand_total"].ToString());
                invoice1.SetParameterValue("p_date", dr["date"].ToString());
                crystalReportViewer1.ReportSource = invoice1;


            }
        }
    }
}
