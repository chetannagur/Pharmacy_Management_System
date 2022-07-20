using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo_project.DAL;
using demo_project.BLL;
namespace demo_project.UI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        DAL_medicine dal_med = new DAL_medicine();
        DAL_employee dal_emp = new DAL_employee();
        float sale;
        Dal_transaction dal_transaction = new Dal_transaction();
        private void Home_Load(object sender, EventArgs e)
        {
            DataTable dt = dal_med.expiry();
            lbl_exmed.Text = dt.Rows.Count.ToString();

            DataTable dt1 = dal_emp.select();
            lbl_emp.Text = dt1.Rows.Count.ToString();

            lbl_ttl_sell.Text = dal_transaction.SumOfTransaction(sale).ToString();
        }
    }
}
