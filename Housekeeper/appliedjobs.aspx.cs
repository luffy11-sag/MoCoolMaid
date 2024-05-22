using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.Housekeeper
{
    public partial class appliedjobs : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["hkemail"])))
            {
                Response.Redirect("~/housekeeperlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            getApplicationDetails();
        }

        void getApplicationDetails()
        {
            int hk_id = Convert.ToInt32(Session["hkid"]);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ccmd = con.CreateCommand();
            ccmd.CommandType = CommandType.Text;
            ccmd.Parameters.AddWithValue("@hkid", hk_id);
            ccmd.CommandText = "SELECT u.User_FName AS fname, ";
            ccmd.CommandText += "u.User_LName AS lname, ";
            ccmd.CommandText += "u.User_Email AS email, ";
            ccmd.CommandText += "ho.HO_Status AS hostatus, ";
            ccmd.CommandText += "ho.HO_ID AS HOID, ";
            ccmd.CommandText += "ja.Job_ID AS jobid, ";
            ccmd.CommandText += "ja.Application_Date AS appdate, ";
            ccmd.CommandText += "ja.Application_Status AS tumstatus, ";
            ccmd.CommandText += "cat.JCategory_Name AS jname, ";
            ccmd.CommandText += "j.Job_Status AS jstatus, ";
            ccmd.CommandText += "j.Job_ID AS JobId ";
            ccmd.CommandText += "FROM tblUser u, tblHouseowner ho, tblHousekeeper hk, tblJobApplication ja, tblJob j, tblJobCategory cat ";
            ccmd.CommandText += "WHERE u.User_ID = ho.HO_User_ID ";
            ccmd.CommandText += "AND ja.Job_ID = j.Job_ID ";
            ccmd.CommandText += "AND j.HO_ID = ho.HO_ID ";
            ccmd.CommandText += "AND ja.HK_ID = hk.HK_ID ";
            ccmd.CommandText += "AND j.JCategory_ID = cat.JCategory_ID ";            
            ccmd.CommandText += "AND ho.HO_Status = '1' ";
            ccmd.CommandText += "AND ja.HK_ID = @hkid ";
            
            SqlDataAdapter sqlda = new SqlDataAdapter(ccmd);
            DataTable dta = new DataTable();
            sqlda.Fill(dta);
            con.Close();

            if (dta.Rows.Count > 0)
            {
                rptApplications.DataSource = dta;
                rptApplications.DataBind();
            }
            else
            {
                rptApplications.Visible = false;
                pnlNoApp.Visible = true;
            }
        }

        protected string GetApplicationStatusText(object status)
        {
            int applicationStatus = Convert.ToInt32(status);

            switch (applicationStatus)
            {
                case 0:
                    return "Pending";
                case 1:
                    return "Accepted";
                case 2:
                    return "Rejected";
                default:
                    return "Unknown";
            }
        }

        protected void btnBrowseJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Housekeeper/browseJobs.aspx");
        }
    }
}