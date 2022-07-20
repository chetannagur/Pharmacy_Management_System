using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using demo_project.BLL;
using demo_project.DAL;
namespace demo_project.UI
{
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
        }
        BLL_medicine bll_med = new BLL_medicine();
        DAL_medicine dal_med = new DAL_medicine();
       


        private void btn_add_Click(object sender, EventArgs e)
        {
            bll_med.med_name = txt_med_name.Text;
            bll_med.med_manifacturer = txt_manufacturer.Text;
            bll_med.med_expdate = Convert.ToDateTime(dateTimePicker1.Value);
            bll_med.med_buyingprice = float.Parse(txt_buying_price.Text);
            bll_med.med_sellingprice = float.Parse(txt_selling_price.Text);
            bll_med.med_quantity = int.Parse(txt_quantity.Text);
            bool success = dal_med.insert(bll_med);
            if (success == true)
            {
                MessageBox.Show("Data Inserted Successfuly");
            }
            else
            {
                MessageBox.Show("Data Not Inserted");
            }
            DataTable dt = dal_med.select();
            dataGridView1.DataSource = dt;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            bll_med.med_id = int.Parse(txt_med_id.Text);
            bll_med.med_name = txt_med_name.Text;
            bll_med.med_manifacturer = txt_manufacturer.Text;
            bll_med.med_expdate = Convert.ToDateTime(dateTimePicker1.Value);
            bll_med.med_buyingprice = float.Parse(txt_buying_price.Text);
            bll_med.med_sellingprice = float.Parse(txt_selling_price.Text);
            bll_med.med_quantity = int.Parse(txt_quantity.Text);
            bool success = dal_med.update(bll_med);
            if (success == true)
            {
                MessageBox.Show("Data updated Successfuly");
            }
            else
            {
                MessageBox.Show("Data Not updated");
            }
            DataTable dt = dal_med.select();
            dataGridView1.DataSource = dt;
        }

        private void btn_delete_Click(object sender, EventArgs e)
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
            DataTable dt = dal_med.select();
            dataGridView1.DataSource = dt;
        }

        private void Medicines_Load(object sender, EventArgs e)
        {
            DataTable dt = dal_med.select();
            dataGridView1.DataSource = dt;
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int med_id, quantity;
            double buying_price, selling_price;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                med_id = Convert.ToInt16(row.Cells[0].Value.ToString());
                buying_price = Convert.ToDouble(row.Cells[4].Value.ToString());
                selling_price = Convert.ToDouble(row.Cells[5].Value.ToString());
                quantity = Convert.ToInt16(row.Cells[6].Value.ToString());
                txt_med_name.Text = row.Cells[1].Value.ToString();
                txt_manufacturer.Text = row.Cells[2].Value.ToString();
                txt_buying_price.Text = buying_price.ToString();
                txt_selling_price.Text = selling_price.ToString();
                txt_med_id.Text = med_id.ToString();
                txt_quantity.Text = quantity.ToString();

            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keywords = txt_search.Text;
            if (keywords != null)
            {
                DataTable dt = dal_med.search(keywords);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = dal_med.select();
                dataGridView1.DataSource = dt;
            }
        }
    }
}
