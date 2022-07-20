using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace demo_project.DAL
{
    class Dal_Transaction_details
    {
        static string Conn=ConfigurationManager.ConnectionStrings["pharmacydb"].ConnectionString;
        #region Insert Into Transaction Table
        public bool insert(BLL.BLL_transactionDetails td)
        {
            SqlConnection con = new SqlConnection(Conn);
            bool isSuccess = false;
            try
            {
                con.Open();
                string sql = "insert into tbl_transaction_details (inv_id,med_id,med_name,med_quantity,med_selling_price,med_total_price,date) values (@inv_id,@med_id,@med_name,@med_quantity,@selling_price,@total_price,@date)";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@inv_id",td.inv_id);
                cmd.Parameters.AddWithValue("@med_id", td.med_id);
                cmd.Parameters.AddWithValue("@med_name", td.med_name);
                cmd.Parameters.AddWithValue("@med_quantity", td.quantity);
                cmd.Parameters.AddWithValue("@selling_price", td.selling_price);
                cmd.Parameters.AddWithValue("@total_price", td.total_price);
                cmd.Parameters.AddWithValue("@date", td.date);
                int row = cmd.ExecuteNonQuery();
                if(row>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("error1"+e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region search Medicine for sell
        public BLL.BLL_medicine SearchForBill(string keywords)
        {
            BLL.BLL_medicine medicine = new BLL.BLL_medicine();
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select * from tbl_medicine where med_name like'%"+keywords+"%' ";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    medicine.med_name=dt.Rows[0]["med_name"].ToString();
                    medicine.med_id = int.Parse(dt.Rows[0]["med_id"].ToString());
                    medicine.med_sellingprice = float.Parse(dt.Rows[0]["med_sellingprice"].ToString());
                }
                else
                {
                    MessageBox.Show("Wrong name !! Search Again");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return medicine;
        }
        #endregion
        #region Get Invoice List
        public DataSet Invoice_product(int invid)
        {

            SqlConnection con = new SqlConnection(Conn);
            string sql = "SELECT * FROM tbl_transaction_details WHERE inv_id=@invoice_id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@invoice_id", invid);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;


        }
        #endregion
    }
}
