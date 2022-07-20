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
    public partial class expire_medicines : Form
    {
        public expire_medicines()
        {
            InitializeComponent();
        }
        BLL.BLL_medicine bll_med = new BLL.BLL_medicine();
        DAL.DAL_medicine dal_med = new DAL.DAL_medicine();
        private void expire_medicines_Load(object sender, EventArgs e)
        {
            DataTable dt = dal_med.expiry();
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bll_med.med_id = int.Parse(txt_med_id.Text);
            bool success = dal_med.delete(bll_med);
            if (success == true)
            {
                MessageBox.Show("Data Deleted");
            }
            else
            {
                MessageBox.Show("Data Not Deleted");
            }
            DataTable dt = dal_med.expiry();
            dataGridView1.DataSource = dt;
        }
    }
}
