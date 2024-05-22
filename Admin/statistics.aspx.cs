using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.Admin
{
    public partial class statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["adminun"])))
            {
                Response.Redirect("~/adminlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }

            if (!IsPostBack)
            {
                int housekeeperCount = HousekeeperCount();
                int houseownerCount = HouseownerCount();
                int jobsPostedCount = jobsPosted();
                int appSubmittedCount = appSubmitted();
                int appSuccessCount = appSuccess();
                int blockedUsersCount = blockedUsers();

                PieChart1.PieChartValues[0].Data = housekeeperCount;
                PieChart1.PieChartValues[1].Data = houseownerCount;

                PieChart1.DataBind();

                lblJobsPosted.Text = jobsPostedCount.ToString();
                lblAppSubmitted.Text = appSubmittedCount.ToString();
                lblAppSuccess.Text = appSuccessCount.ToString();
                lblUsersBlocked.Text = blockedUsersCount.ToString();
            }
        }

        private int HousekeeperCount()
        {
            string query = "SELECT * FROM tblHousekeeper";
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }

        private int HouseownerCount()
        {
            string query = "SELECT * FROM tblHouseowner";
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }

        private int jobsPosted()
        {
            string query = "SELECT * FROM tblJob";
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }

        private int appSubmitted()
        {
            string query = "SELECT * FROM tblJobApplication";
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }

        private int appSuccess()
        {
            string query = "SELECT * FROM tblJobApplication WHERE Application_Status = 1";
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }

        private int blockedUsers()
        {
            string query = "SELECT HK_Status FROM tblHousekeeper WHERE HK_Status = 0 " +
                   "UNION " +
                   "SELECT HO_Status FROM tblHouseowner WHERE HO_Status = 0"; ;
            DataTable dt = GetData(query);
            return dt.Rows.Count;
        }
        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
    }
}