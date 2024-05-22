using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace MoCoolMaid.HouseOwner
{
    public partial class updateJob : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["hoemail"])))
            {
                Response.Redirect("~/houseownerlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            if (!IsPostBack)
            {
                bindJobCategory();
                bindJobDistrict();
                BindJobDetails();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJobId.Text))
            {
                lblMsg.Text = "Please select record to update";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            Boolean IsUpdated = false;
            string strDate = txtJobDeadline.Text;
            DateTime dt = Convert.ToDateTime(strDate);
            SqlConnection con = new SqlConnection(_conString);
            int job_id = Convert.ToInt32(txtJobId.Text);
            SqlCommand scmd = new SqlCommand();
            scmd.CommandType = CommandType.Text;

            scmd.CommandText = "UPDATE tblJob SET Job_Desc = @desc, Job_Deadline = @deadline, Job_Status = @status, Job_Salary = @salary, JCategory_ID = @jtype, Loc_ID = @district WHERE Job_ID = @jobid";
            scmd.Parameters.AddWithValue("@jobid", job_id);
            scmd.Parameters.AddWithValue("@jtype", ddlJobType.SelectedValue);
            scmd.Parameters.AddWithValue("@desc", txtJobDescription.Text.Trim());
            scmd.Parameters.AddWithValue("@deadline", dt);
            scmd.Parameters.AddWithValue("@status", ddlJobStatus.SelectedValue);
            scmd.Parameters.AddWithValue("@salary", txtJobSalary.Text.Trim());
            scmd.Parameters.AddWithValue("@district", ddlJobDistrict.SelectedValue);
            scmd.Connection = con;
            con.Open();

            IsUpdated = scmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                Response.Redirect("~/HouseOwner/viewPostedJobs.aspx");            
            }
            else
            {
                lblMsg.Text = "Error while updating Job";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ResetAll();
        }

        private void BindJobDetails()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["JobId"]) && int.TryParse(Request.QueryString["JobId"], out int jobId))
            {               
                int jobIdValue = jobId;
                
                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@jobid", jobIdValue);
                cmd.CommandText = "SELECT * FROM tblJob WHERE Job_ID = @jobid";
                SqlDataReader dr;
                
                con.Open();
                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    DateTime jobDeadline = (DateTime)dr["Job_Deadline"];
                    ddlJobType.SelectedValue = dr["JCategory_ID"].ToString();
                    txtJobId.Text = dr["Job_ID"].ToString();
                    ddlJobType.SelectedIndex = Convert.ToInt32(dr["JCategory_ID"]);
                    txtJobDescription.Text = dr["Job_Desc"].ToString();
                    txtJobDeadline.Text = jobDeadline.ToString("yyyy-MM-dd");
                    txtJobSalary.Text = dr["Job_Salary"].ToString();
                    ddlJobStatus.SelectedValue = dr["Job_Status"].ToString();
                }
                con.Close();

            }           
        }

        private void bindJobCategory()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJobCategory";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobCategories = new DataTable();
            using (da)
            {
                da.Fill(dtJobCategories);
            }
            ddlJobType.DataSource = dtJobCategories;
            ddlJobType.DataTextField = "JCategory_Name";
            ddlJobType.DataValueField = "JCategory_ID";
            ddlJobType.DataBind();
        }

        private void bindJobDistrict()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblLocation";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobDistrict = new DataTable();
            using (da)
            {
                da.Fill(dtJobDistrict);
            }
            ddlJobDistrict.DataSource = dtJobDistrict;
            ddlJobDistrict.DataTextField = "District";
            ddlJobDistrict.DataValueField = "Loc_ID";
            ddlJobDistrict.DataBind();
        }

        private void ResetAll()
        {
            ddlJobType.SelectedIndex = 0;
            txtJobDescription.Text = "";
            txtJobSalary.Text = "";
            txtJobDeadline.Text = "";
            ddlJobDistrict.SelectedIndex = 0;
        }
    }   
}


