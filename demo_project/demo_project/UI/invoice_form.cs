using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo_project.UI
{
    public partial class invoice_form : Form
    {
        public invoice_form()
        {
            InitializeComponent();
        }
        int invoice_id;
        DAL.Dal_Transaction_details tdd = new DAL.Dal_Transaction_details();
        DAL.Dal_transaction tddal = new DAL.Dal_transaction();
        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                invoice_id = int.Parse(txt_invoice_id.Text);
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

                    bill_Reports1.SetDataSource(list);
                    bill_Reports1.SetParameterValue("p_inv_no", dr["inv_id"].ToString());
                    bill_Reports1.SetParameterValue("p_grand_total", dr["grand_total"].ToString());
                    bill_Reports1.SetParameterValue("p_date", dr["date"].ToString());
                    crystalReportViewer1.ReportSource = bill_Reports1;


                }
            }

            catch(Exception ae)
            {
                MessageBox.Show(ae.Message);
            }
 
        }
    }
}
