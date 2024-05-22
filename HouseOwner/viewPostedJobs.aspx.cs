using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.HouseOwner
{
    public partial class viewPostedJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["hoemail"])))
            {
                Response.Redirect("~/houseownerlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            if (!IsPostBack)
            {
                bindPostedJobs();
            }
        }

        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        private void bindPostedJobs()
        {
            int ho_id = Convert.ToInt32(Session["hoid"]);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJob j, tblJobCategory jc, tblLocation l WHERE j.JCategory_ID = jc.JCategory_ID AND j.Loc_ID = l.Loc_ID AND j.HO_ID = @hoid";
            cmd.Parameters.AddWithValue("@hoid", ho_id);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobs = new DataTable();

            using (da)
            {
                da.Fill(dtJobs);
            }

            lvPostedJobs.DataSource = dtJobs;
            lvPostedJobs.DataBind();
        }

        protected void lnkUpdateJob_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdateJob = (LinkButton)sender;

            string jobID = lnkUpdateJob.CommandArgument;

            Response.Redirect($"updateJob.aspx?jobID={jobID}");
        }

        protected string GetStatusDescription(int status)
        {
            switch (status)
            {
                case 1:
                    return "Pending";
                case 2:
                    return "Occupied";
                case 3:
                    return "Vacant";
                default:
                    return "Unknown";
            }
        }

        protected void lnkChangeStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkChangeStatus = (LinkButton)sender;
            string jobID = lnkChangeStatus.CommandArgument;          
            txtJobID.Text = jobID;
            int job_id = Convert.ToInt32(jobID);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@jobid", job_id);
            cmd.CommandText = "SELECT * FROM tblJob WHERE Job_ID = @jobid";
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {               
                ddlStatus.SelectedValue = dr["Job_Status"].ToString();
            }
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "statusModal();", true);
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            int job_id = Convert.ToInt32(txtJobID.Text);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand scmd = new SqlCommand();
            scmd.CommandType = CommandType.Text;

            scmd.CommandText = "UPDATE tblJob SET Job_Status = @status WHERE Job_ID = @jobid";
            scmd.Parameters.AddWithValue("@jobid", job_id);
            scmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            scmd.Connection = con;
            con.Open();

            IsUpdated = scmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                bindPostedJobs();
            }
            else
            {
                lblMsg.Text = "Error while updating Status";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}