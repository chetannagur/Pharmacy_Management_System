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
    class Dal_transaction
    {
        static string Conn=ConfigurationManager.ConnectionStrings["pharmacydb"].ConnectionString;
        #region Generate Auto Invoice Id
        public string auto_invoice(string i)
        {
            SqlConnection con = new SqlConnection(Conn);
            try
            {
            con.Open();
            string  Query = "Select MAX(inv_id)+1 from tbl_transaction ";
            SqlCommand cmd = new SqlCommand(Query,con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
            {
                i = dr[0].ToString();

                if(i=="")
                {
                    i = "1";
                }
            }
               
            }
            else
            {
                i = "1";
                
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
            
            return i;
            
        }
        #endregion
        #region Insert Transaction
        public bool insert_transaction(BLL.BLL_transaction t)
        {
            SqlConnection con = new SqlConnection(Conn);
            bool isSuccess = false;
            try
            {
                con.Open();
                string sql = "set identity_insert tbl_transaction on insert into tbl_transaction (inv_id,grand_total,date) values (@inv_id,@grand_total,@date); SELECT @@IDENTITY;";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@inv_id",t.inv_id);
                cmd.Parameters.AddWithValue("@grand_total",t.grand_total);
                cmd.Parameters.AddWithValue("@date",t.date);
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
                MessageBox.Show("error0"+e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Get Sum of transaction
        public float SumOfTransaction(float sum)
        {
            
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select SUM(grand_total) as grand_total from tbl_transaction group by year(date),month(date)";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    sum = float.Parse(dt.Rows[0]["grand_total"].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return sum;
        }
        #endregion
        #region Get Invoice List
        public DataSet Invoice_product(int invid)
        {

            SqlConnection con = new SqlConnection(Conn);
            string sql = "SELECT * FROM tbl_transaction WHERE inv_id=@invoice_id";
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
