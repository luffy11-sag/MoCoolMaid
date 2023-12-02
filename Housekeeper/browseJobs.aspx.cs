using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace MoCoolMaid.Housekeeper
{
    public partial class browseJobs : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }
    }
}