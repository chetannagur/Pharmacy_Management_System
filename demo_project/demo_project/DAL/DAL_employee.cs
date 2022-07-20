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
    
    class DAL_employee
    {
        static string Conn = ConfigurationManager.ConnectionStrings["pharmacydb"].ConnectionString;
        public DataTable select()
        {
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select * from tbl_emp";
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
        #region insert data into table
        public bool insert(BLL.BLL_employee u)
        {
            bool isSuccess=false;
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql="insert into tbl_emp (emp_name,emp_contact,emp_email,emp_address,emp_city,emp_salary) values(@empname,@empcontact,@empemail,@empaddress,@empcity,@empsalary)" ;
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@empname",u.emp_name);
                cmd.Parameters.AddWithValue("@empemail", u.emp_email);
                cmd.Parameters.AddWithValue("@empcontact", u.emp_contact);
                cmd.Parameters.AddWithValue("@empcity", u.emp_city);
                cmd.Parameters.AddWithValue("@empaddress", u.emp_address);
                cmd.Parameters.AddWithValue("@empsalary", u.emp_salary);
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
        #region Update data into database
        public bool update(BLL.BLL_employee u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql = "update tbl_emp set  emp_name=@empname,emp_contact=@empcontact,emp_email=@empemail,emp_address=@empaddress,emp_city=@empcity,emp_salary=@empsalary where emp_id=@empid";
                SqlCommand cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@empid",u.emp_id);
                cmd.Parameters.AddWithValue("@empname",u.emp_name);
                cmd.Parameters.AddWithValue("@empcontact", u.emp_contact);
                cmd.Parameters.AddWithValue("@empemail",u.emp_email);
                cmd.Parameters.AddWithValue("@empaddress",u.emp_address);
                cmd.Parameters.AddWithValue("@empcity",u.emp_city);
                cmd.Parameters.AddWithValue("@empsalary",u.emp_salary);
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
        #region delete data from database
        public bool delete(BLL.BLL_employee u)
        {
            bool isSuccess = false;
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql="delete from tbl_emp where emp_id=@empid";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@empid",u.emp_id);
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
        #region Search data into database
        public DataTable search(string keywords)
        {
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "select * from tbl_emp where emp_name Like '%"+keywords+"%' or emp_id like '%"+keywords+"%'";
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
        
    }
}
