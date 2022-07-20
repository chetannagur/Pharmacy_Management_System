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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
        }

        BLL_employee bll_emp = new BLL_employee();
        DAL_employee dal_emp = new DAL_employee();
        public void clear()
        {
            txt_emp_id.Text = "";
            txt_emp_name.Text = "";
            txt_emp_cnt.Text = "";
            txt_emp_email.Text = "";
            txt_emp_address.Text = "";
            txt_emp_city.Text = "";
            txt_emp_salary.Text = "";
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            
            bll_emp.emp_name = txt_emp_name.Text;
            bll_emp.emp_contact = txt_emp_cnt.Text;
            bll_emp.emp_email = txt_emp_email.Text;
            bll_emp.emp_address = txt_emp_address.Text;
            bll_emp.emp_city = txt_emp_city.Text;
            bll_emp.emp_salary = float.Parse(txt_emp_salary.Text);
            bool success = dal_emp.insert(bll_emp);
            if(success==true)
            {
                MessageBox.Show("Data Inserted Successfully");
            }
            else
            {
                MessageBox.Show("Data Not Inserted");
            }
            DataTable dt = dal_emp.select();
            dataGridView1.DataSource = dt;
            clear();
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            DataTable dt =  dal_emp.select();
            dataGridView1.DataSource = dt;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            bll_emp.emp_id =int.Parse( txt_emp_id.Text);
            bll_emp.emp_name = txt_emp_name.Text;
            bll_emp.emp_contact = txt_emp_cnt.Text;
            bll_emp.emp_email = txt_emp_email.Text;
            bll_emp.emp_address = txt_emp_address.Text;
            bll_emp.emp_city = txt_emp_city.Text;
            bll_emp.emp_salary = float.Parse(txt_emp_salary.Text);
            bool success = dal_emp.update(bll_emp);
            if(success==true)
            {
                MessageBox.Show("Data Updated Successfully");

            }
            else
            {
                MessageBox.Show("Data Not Updated");
            }
            DataTable dt = dal_emp.select();
            dataGridView1.DataSource = dt;
            clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            bll_emp.emp_id = int.Parse(txt_emp_id.Text);
            bool success = dal_emp.delete(bll_emp);
            if(success==true)
            {
                MessageBox.Show("Data Deleted Successfully");
            }
            else
            {
                MessageBox.Show("Data Not Deleted");
            }
            DataTable dt = dal_emp.select();
            dataGridView1.DataSource = dt;
            clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int emp_id;
            double emp_salary;
            if(e.RowIndex >=0)
            {
                try
                {
                    DataGridViewRow row=this.dataGridView1.Rows[e.RowIndex];
                    emp_id=Convert.ToInt16(row.Cells[0].Value.ToString());
                    txt_emp_name.Text=row.Cells[1].Value.ToString();
                    txt_emp_cnt.Text=row.Cells[2].Value.ToString();
                    txt_emp_email.Text=row.Cells[3].Value.ToString();
                    txt_emp_address.Text=row.Cells[4].Value.ToString();
                    txt_emp_city.Text=row.Cells[5].Value.ToString();
                    emp_salary=Convert.ToDouble(row.Cells[6].Value.ToString());
                    txt_emp_id.Text=Convert.ToString(emp_id);
                    txt_emp_salary.Text=Convert.ToString(emp_salary);
                    
                }
                catch(Exception ae)
                {
                    MessageBox.Show(ae.Message);
                }
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string keywords = txt_search.Text;
            if (keywords != null)
            {
                DataTable dt = dal_emp.search(keywords);
                dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = dal_emp.select();
                dataGridView1.DataSource = dt;
            }
        }
    }
}
