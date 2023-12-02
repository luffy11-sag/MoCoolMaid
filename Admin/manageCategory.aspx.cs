using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace MoCoolMaid.Admin
{
    public partial class manageCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCategoryData();
        }
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        private void BindCategoryData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJobCategory";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtCat = new DataTable();
            using (da)
            {
                da.Fill(dtCat);
            }

            gvs.DataSource = dtCat;
            gvs.DataBind();
        }

        protected void gvs_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategoryId.Text = (gvs.DataKeys[gvs.SelectedIndex].Value.ToString());
            txtCategoryName.Text = ((Label)gvs.SelectedRow.FindControl("lblCatName")).Text;

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@catid", txtCategoryId.Text.Trim());
            //assign the parameter to the sql statement
            cmd.CommandText = "SELECT * FROM tblJobCategory where JCategory_ID = @catid";
            con.Open();
            cmd.ExecuteReader();
            con.Close();
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Boolean IsAdded = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //assign the parameter to the sql statement
            cmd.CommandText = "SELECT * FROM tblJobCategory WHERE JCategory_Name = @catname";
            cmd.Parameters.AddWithValue("@catname", txtCategoryName.Text.Trim());
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblMsg.Text = "The category already exists!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                dr.Close();
                SqlCommand scmd = new SqlCommand();
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "INSERT INTO tblJobCategory(JCategory_Name) VALUES (@catname)";
                scmd.Parameters.AddWithValue("@catname", txtCategoryName.Text.Trim());
                IsAdded = scmd.ExecuteNonQuery() > 0;
                con.Close();
                if (IsAdded)
                {
                    lblMsg.Text = txtCategoryName.Text + " category added successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindCategoryData();
                }
                else
                {
                    lblMsg.Text = "Error while adding category " + txtCategoryName.Text;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                ResetAll();

            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryId.Text))
            {
                lblMsg.Text = "Please select record to update";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM tblJobCategory WHERE JCategory_Name = @catname";
            cmd.Parameters.AddWithValue("@catname", txtCategoryName.Text.Trim());
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblMsg.Text = "The category already exists!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                dr.Close();
                int cat_id = Convert.ToInt32(txtCategoryId.Text);
                SqlCommand scmd = new SqlCommand();
                scmd.CommandType = CommandType.Text;

                scmd.CommandText = "UPDATE tblJobCategory SET JCategory_Name = @catname WHERE JCategory_ID = @catid";
                scmd.Parameters.AddWithValue("@catid", cat_id);
                scmd.Parameters.AddWithValue("@catname", txtCategoryName.Text.Trim());
                scmd.Connection = con;

                IsUpdated = scmd.ExecuteNonQuery() > 0;

                con.Close();
                if (IsUpdated)
                {
                    lblMsg.Text = txtCategoryName.Text + " category updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindCategoryData();
                }
                else
                {
                    lblMsg.Text = "Error while updating category " + txtCategoryName.Text;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                ResetAll();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryId.Text))
            {
                lblMsg.Text = "Please select category to delete";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            Boolean IsDeleted = false;
            int cat_id = Convert.ToInt32(txtCategoryId.Text);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM tblJobCategory WHERE JCategory_ID = @catid";
            cmd.Parameters.AddWithValue("@catid", cat_id);
            cmd.Connection = con;
            con.Open();

            IsDeleted = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsDeleted)
            {
                lblMsg.Text = txtCategoryName.Text + " category deleted successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindCategoryData();
            }
            else
            {
                lblMsg.Text = "Error while deleting category " + txtCategoryName.Text;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ResetAll();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
            btnAdd.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }
    }
}