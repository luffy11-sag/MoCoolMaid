using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.Admin
{
    public partial class manageJobs : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["adminun"])))
            {
                Response.Redirect("~/adminlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            if (!IsPostBack)
            {
                BindJobsData();
            }
        }

        protected void lvJobs_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindJobsData();
        }

        private void BindJobsData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJob j, tblJobCategory jc, tblLocation l, tblHouseowner ho, tblUser u WHERE j.JCategory_ID = jc.JCategory_ID AND j.Loc_ID = l.Loc_ID AND j.HO_ID = ho.HO_ID AND ho.HO_User_ID = u.User_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobs = new DataTable();

            using (da)
            {
                da.Fill(dtJobs);
            }

            lvJobs.DataSource = dtJobs;
            lvJobs.DataBind();
        }

        protected void lnkDeleteJobs_Click(object sender, EventArgs e)
        {
            LinkButton lnkDeleteJob = (LinkButton)sender;
            string job_id = lnkDeleteJob.CommandArgument;

            Boolean IsDeleted = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM tblJob WHERE Job_ID = @jobid";
            cmd.Parameters.AddWithValue("@jobid", job_id);
            cmd.Connection = con;
            con.Open();

            IsDeleted = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsDeleted)
            {
                lblMsg.Text ="Job deleted successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindJobsData();
            }
            else
            {
                lblMsg.Text = "Error while deleting job";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}