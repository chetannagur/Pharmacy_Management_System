using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
namespace demo_project.DAL
{
    class DAL_medicine
    {
        static string Conn=ConfigurationManager.ConnectionStrings["pharmacydb"].ConnectionString;
        #region Select Data from database
        public DataTable select()
        {
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "Select * from tbl_medicine";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        #endregion
        #region Insert Data Into Database
         public bool insert(BLL.BLL_medicine u)
        {
            SqlConnection con = new SqlConnection(Conn);
            bool isSuccess = false;
            try
            {
                con.Open();
                string sql = "insert into tbl_medicine (med_name,med_manufacturer,med_expdate,med_buyingprice,med_sellingprice,med_quantity) values(@name,@manufacturer,@expdate,@buyingprice,@sellingprice,@quantity)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name",u.med_name);
                cmd.Parameters.AddWithValue("@manufacturer",u.med_manifacturer);
                cmd.Parameters.AddWithValue("@expdate", u.med_expdate);
                cmd.Parameters.AddWithValue("@buyingprice",u.med_buyingprice);
                cmd.Parameters.AddWithValue("@sellingprice", u.med_sellingprice);
                cmd.Parameters.AddWithValue("@quantity",u.med_quantity);
                int count = cmd.ExecuteNonQuery();
                if(count>0)
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
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Update Data Into Database
        public bool update(BLL.BLL_medicine u)
         {
             bool isSuccess = false;
             SqlConnection con = new SqlConnection(Conn);
             try
             {
                 con.Open();
                 string sql = "update tbl_medicine set med_name=@name,med_manufacturer=@manufacturer,med_expdate=@expdate,med_buyingprice=@buyingprice,med_sellingprice=@sellingprice,med_quantity=@quantity where med_id=@medid";
                 SqlCommand cmd = new SqlCommand(sql,con);
                 cmd.Parameters.AddWithValue("@medid", u.med_id);
                 cmd.Parameters.AddWithValue("@name", u.med_name);
                 cmd.Parameters.AddWithValue("@manufacturer", u.med_manifacturer);
                 cmd.Parameters.AddWithValue("@expdate", u.med_expdate);
                 cmd.Parameters.AddWithValue("@buyingprice", u.med_buyingprice);
                 cmd.Parameters.AddWithValue("@sellingprice", u.med_sellingprice);
                 cmd.Parameters.AddWithValue("@quantity", u.med_quantity);
                 int count = cmd.ExecuteNonQuery();
                 if(count>0)
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
                 MessageBox.Show(e.Message);
             }
            finally
             {
                 con.Close();
             }
             return isSuccess;
         }
        #endregion
        #region Delete Data From Database
        public bool delete(BLL.BLL_medicine u)
        {
            bool isSuccess=false;
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql = "delete from tbl_medicine where med_id=@medid";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@medid",u.med_id);
                int count = cmd.ExecuteNonQuery();
                if(count>0)
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
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Search Data Into Database
        public DataTable search(string keywords)
        {
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql="select * from tbl_medicine Where med_id like '%"+keywords+"%' or med_name like '%"+keywords+"%' or med_manufacturer like '%"+keywords+"%'";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        #endregion
        #region Finding Expired Date In Database
        public DataTable expiry()
        {
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select * from tbl_medicine where med_expdate < GETDATE() order by med_expdate ASC";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        #endregion
        #region Get Id Based On Medicine Name
        public BLL.BLL_medicine GetIdBasedOnName(string Name)
        {
            BLL.BLL_medicine medicine = new BLL.BLL_medicine();
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select med_id from tbl_medicine where med_name='"+Name+"'";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    medicine.med_id=int.Parse(dt.Rows[0]["med_id"].ToString());
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
            return medicine;
        }
        #endregion
        #region Select  Quantity From Table
        public int Select_Quantity(int med_id)
        {
            SqlConnection con = new SqlConnection(Conn);
            int qty = 0;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select med_quantity from tbl_medicine where med_id='"+med_id+"'";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if(dt.Rows.Count>0){
                    qty = int.Parse(dt.Rows[0]["med_quantity"].ToString());
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
            return qty;
        }
        #endregion
        #region Update Quantity In Table
        public bool update_quantity(int med_id,int qty)
        {
            SqlConnection con = new SqlConnection(Conn);
            bool isSuccess = false;
            try
            {
                con.Open();
                string sql = "update tbl_medicine set med_quantity=@medqty where med_id=@med_id";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@medqty",qty);
                cmd.Parameters.AddWithValue("@med_id",med_id);
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
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Decrease Quantity From Table
        public bool decrease_quantity(int med_id,int qty)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(Conn);

            try
            {

                con.Open();
                int current_qty = Select_Quantity(med_id);
                int new_qty = current_qty - qty;
                

                isSuccess = update_quantity(med_id, new_qty);


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }
        #endregion
    }
}
