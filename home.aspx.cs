using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace MoCoolMaid
{
    public partial class home : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLocation();
                ListItem defLocation = new ListItem("Select Location", "-1");
                ddlDistrict.Items.Insert(0, defLocation);
                BindCategory();
                ListItem defCategory = new ListItem("Select Category", "-1");
                ddlCategory.Items.Insert(0, defCategory);

            }
            BindTestimonial();
            txtSearch_TextChanged(sender, null);
            if (Session["hoid"] != null)
            {
                pnlJobsAvailable.Visible = false;
            }
            if (Session["hkid"] != null)
            {
                foreach (ListViewItem item in lvJobs.Items)
                {
                    LinkButton btnApply = (LinkButton)item.FindControl("lnkApply");
                    int jobId = Convert.ToInt32(lvJobs.DataKeys[item.DataItemIndex]["Job_ID"]);

                    if (HasUserApplied(jobId))
                    {
                        btnApply.Text = "Application sent";
                    }
                }
            }
        }

        protected void lvJobs_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            txtSearch_TextChanged(sender, null);
        }

        private void BindLocation()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblLocation";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtLocation = new DataTable();

            using (da)
            {
                da.Fill(dtLocation);
            }

            ddlDistrict.DataSource = dtLocation;
            ddlDistrict.DataTextField = "District";
            ddlDistrict.DataValueField = "Loc_ID";
            ddlDistrict.DataBind();
        }

        private void BindCategory()
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
            ddlCategory.DataSource = dtJobCategories;
            ddlCategory.DataTextField = "JCategory_Name";
            ddlCategory.DataValueField = "JCategory_ID";
            ddlCategory.DataBind();
        }

        protected void lnkApply_Click(object sender, EventArgs e)
        {
            if (Session["hkid"] == null)
            {
                Response.Redirect("~/housekeeperlogin.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "housekeeperModal();", true);
            }
            else
            {
                LinkButton btn = (LinkButton)sender;
                int job_id = Convert.ToInt32(btn.CommandArgument.ToString());
                if (chkexist(job_id))
                {

                    lblmsg.Text = "Already applied for this job!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    btn.Text = "Application sent";
                }
                else
                {
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    //add INSERT statement to request access to movie
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into tblJobApplication(Job_Id, HK_ID, Application_Date, Application_Status) " + "values (@jobid, @hkid, @dateaccess, @status)";
                    cmd.Parameters.AddWithValue("@hkid", Session["hkid"]);
                    cmd.Parameters.AddWithValue("@jobid", job_id);
                    cmd.Parameters.AddWithValue("@dateaccess", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", 0);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    btn.Text = "Application sent";
                    lblmsg.Text = "Request sent!";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String strhoname = txtSearch.Text.Trim();
            String strDistrict = ddlDistrict.SelectedValue;
            String strSalary = ddlSalary.SelectedValue;
            String strCategory = ddlCategory.SelectedValue;

            String sqlhoname = "";
            String sqlDistrict = "";
            String sqlSalary = "";
            String sqlCategory = "";

            if (strCategory != "-1")
            {
                sqlCategory = " AND j.JCategory_ID = @category";
            }
            if (strhoname != null)
            {
                sqlhoname = " AND (u.User_FName LIKE @name + '%' OR u.User_LName LIKE @name + '%')";
            }
            if (strDistrict != "-1")
            {
                sqlDistrict = " AND u.User_City = @districts";
            }
            if (strSalary != "-1")
            {
                if (ddlSalary.SelectedValue == "1")
                {
                    sqlSalary = " AND j.Job_Salary > 10000";
                }
                else if (ddlSalary.SelectedValue == "2")
                {
                    sqlSalary = " AND j.Job_Salary > 20000";
                }
                else if (ddlSalary.SelectedValue == "3")
                {
                    sqlSalary = " AND j.Job_Salary > 25000";
                }
            }


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJob j, tblJobCategory jc, tblLocation l, tblHouseowner ho, tblUser u WHERE j.JCategory_ID = jc.JCategory_ID AND j.Loc_ID = l.Loc_ID AND j.HO_ID = ho.HO_ID AND ho.HO_User_ID = u.User_ID" + sqlhoname + sqlDistrict + sqlSalary + sqlCategory;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@name", strhoname);
            cmd.Parameters.AddWithValue("@districts", strDistrict);
            cmd.Parameters.AddWithValue("@category", strCategory);
            DataTable dtJobs = new DataTable();

            using (da)
            {
                da.Fill(dtJobs);
            }

            lvJobs.DataSource = dtJobs;
            lvJobs.DataBind();
        }

        private Boolean chkexist(int x)
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //search for user
            cmd.CommandText = "select * from tblJobApplication where Job_ID=@jobid and HK_ID = @hkid";
            cmd.Connection = con;
            //create a parameterized query
            cmd.Parameters.AddWithValue("@hkid", Session["hkid"]);
            cmd.Parameters.AddWithValue("@jobid", x);
            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            //Check if user subscription already exists in the table
            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BindTestimonial()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser u, tblTestimonial t WHERE u.User_ID = t.User_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtTestimonial = new DataTable();

            using (da)
            {
                da.Fill(dtTestimonial);
            }

            rptTestimonials.DataSource = dtTestimonial;
            rptTestimonials.DataBind();
        }

        private bool HasUserApplied(int jobId)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) FROM tblJobApplication WHERE HK_ID = @hkid AND Job_ID = @jobid";
            cmd.Parameters.AddWithValue("@hkid", Session["hkid"]);
            cmd.Parameters.AddWithValue("@jobid", jobId);

            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }
    }
}