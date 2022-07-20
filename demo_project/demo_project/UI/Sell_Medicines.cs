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
using demo_project.BLL;
using demo_project.DAL;
namespace demo_project.UI
{
    public partial class Sell_Medicines : Form
    {
        public Sell_Medicines()
        {
            InitializeComponent();
        }
        DataTable tdt = new DataTable();
        Dal_transaction dal = new Dal_transaction();
        Dal_Transaction_details tdal = new Dal_Transaction_details();
        DAL_medicine dal_med = new DAL_medicine();
        float total,price,subtotal;
        string inv_id;
        int quantity;
        private void Sell_Medicines_Load(object sender, EventArgs e)
        {
          dateTimePicker1.Value = DateTime.Now.Date;
          txt_invoice_id.Text=  dal.auto_invoice( inv_id);
          data();
        }
       public void data()
        {
            tdt.Columns.Add("Medicine Id");
            tdt.Columns.Add("Medicine Name");
            tdt.Columns.Add("Price");
            tdt.Columns.Add("Quantity");
            tdt.Columns.Add("Total Price");
            dataGridView1.DataSource = tdt;
        }

       private void btn_add_Click(object sender, EventArgs e)
       {
           int med_id;
           
           string med_name;
           med_id = int.Parse(txt_med_id.Text);
           med_name = txt_med_name.Text;
           price = float.Parse(txt_price.Text);
           quantity = int.Parse(txt_quantity.Text);
           subtotal = quantity*price;
           txt_ttl_price.Text = subtotal.ToString();
           total = total + subtotal;
           txt_grand_total.Text = total.ToString();
           if(Name=="")
           {
               MessageBox.Show("Enter Medicine Name");
           }
           else
           {
               tdt.Rows.Add(med_id,med_name,price,quantity,subtotal);
               txt_med_id.Text = "";
               txt_med_name.Text = "";
               txt_price.Text = "";
               txt_quantity.Text = "";
               txt_ttl_price.Text = "0";
           }
           
       }
        public void clear()
       {
           txt_med_id.Text = "";
           txt_med_name.Text = "";
           txt_price.Text = "";
           txt_quantity.Text = "";
           txt_ttl_price.Text = "0";
       }
       private void txt_search_TextChanged(object sender, EventArgs e)
       {
           string keyword = txt_search.Text;
           try
           {
               if (keyword == "")
               {
                   txt_med_id.Text = "";
                   txt_med_name.Text = "";
                   txt_price.Text = "";
                   return;
               }
               else
               {
                   BLL_medicine medicine = tdal.SearchForBill(keyword);
                   txt_med_id.Text = medicine.med_id.ToString();
                   txt_med_name.Text = medicine.med_name.ToString();
                   txt_price.Text = medicine.med_sellingprice.ToString();
                   return;
               }
           }
           catch (Exception )
           {
               return;
           }         
       }

       private void btn_remove_Click(object sender, EventArgs e)
       {
           int rowIndex = dataGridView1.CurrentCell.RowIndex;
           dataGridView1.Rows.RemoveAt(rowIndex);
       }
       private void btn_print_Click(object sender, EventArgs e)
       {
           BLL_transaction transaction = new BLL_transaction();
           transaction.inv_id = int.Parse(txt_invoice_id.Text);
           transaction.grand_total = float.Parse(txt_grand_total.Text);
           transaction.date = DateTime.Now;
           transaction.transactionDetails = tdt;
           bool success = false;
           bool w = dal.insert_transaction(transaction);
           bool x = false;
           for (int i = 0; i < tdt.Rows.Count; i++)
           {
               BLL_transactionDetails transactionDetailBLL = new BLL_transactionDetails();
               string product_name = tdt.Rows[i][1].ToString();
               BLL_medicine medicine = dal_med.GetIdBasedOnName(product_name);
               transactionDetailBLL.med_id = medicine.med_id;
               transactionDetailBLL.inv_id = int.Parse(txt_invoice_id.Text);
               transactionDetailBLL.med_name = tdt.Rows[i][1].ToString();
               transactionDetailBLL.quantity = int.Parse(tdt.Rows[i][3].ToString());
               transactionDetailBLL.selling_price = float.Parse(tdt.Rows[i][2].ToString());
               transactionDetailBLL.total_price = float.Parse(tdt.Rows[i][4].ToString());
               transactionDetailBLL.date = DateTime.Now;
               x = dal_med.decrease_quantity(transactionDetailBLL.med_id, transactionDetailBLL.quantity);
               bool y = tdal.insert(transactionDetailBLL);
               success = w && x && y;
           }
           if (success == true)
           {
               MessageBox.Show("Transaction Completed Successfully");
           }
           else
           {
               MessageBox.Show("Transaction Failed");
           }
           int b = int.Parse(txt_invoice_id.Text);
           clear();
           bill_form inv = new bill_form(b);
           inv.Show();
       }

       private void txt_quantity_TextChanged(object sender, EventArgs e)
       {
          
           if(txt_quantity.Text=="")
           {
               return;
           }
           else{
               txt_ttl_price.Text = ((int.Parse(txt_price.Text)) * (int.Parse(txt_quantity.Text))).ToString();
       }
           }
           
    }
}
