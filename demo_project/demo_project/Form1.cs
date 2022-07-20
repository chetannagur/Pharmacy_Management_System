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
namespace demo_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DAL_medicine dal_med = new DAL_medicine();
        DAL_employee dal_emp = new DAL_employee();
        float sale;
        Dal_transaction dal_transaction = new Dal_transaction();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UI.Home h1 = new UI.Home();
            h1.TopLevel = false;
            pnl_home.Controls.Add(h1);
            h1.BringToFront();
            h1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UI.Employees e1 = new UI.Employees();
            e1.TopLevel = false;
            pnl_home.Controls.Add(e1);
            e1.BringToFront();
            e1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UI.Medicines m1 = new UI.Medicines();
            m1.TopLevel = false;
            pnl_home.Controls.Add(m1);
            m1.BringToFront();
            m1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UI.expire_medicines exm1 = new UI.expire_medicines();
            exm1.TopLevel = false;
            pnl_home.Controls.Add(exm1);
            exm1.BringToFront();
            exm1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UI.Sell_Medicines slm1 = new UI.Sell_Medicines();
            slm1.TopLevel = false;
            pnl_home.Controls.Add(slm1);
            slm1.BringToFront();
            slm1.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = dal_med.expiry();
            lbl_exmed.Text = dt.Rows.Count.ToString();
 
            DataTable dt1 = dal_emp.select();
            lbl_emp.Text = dt1.Rows.Count.ToString();

            lbl_ttl_sell.Text = dal_transaction.SumOfTransaction(sale).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UI.invoice_form i1 = new UI.invoice_form();
            i1.TopLevel = false;
            pnl_home.Controls.Add(i1);
            i1.BringToFront();
            i1.Show();
        }
    }
}
