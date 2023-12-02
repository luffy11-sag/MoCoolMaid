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
    }
}