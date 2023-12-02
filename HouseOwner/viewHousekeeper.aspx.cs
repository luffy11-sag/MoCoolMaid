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
    public partial class viewHousekeeper : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindHousekeeperData();
            }
        }

        private void BindHousekeeperData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHousekeeper hk, tblUser u WHERE hk.HK_User_ID = u.User_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHK = new DataTable();

            using (da)
            {
                da.Fill(dtHK);
            }

            lvHK.DataSource = dtHK;
            lvHK.DataBind();
        }

        protected void lvHK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkViewProfile_Click(object sender, EventArgs e)
        {

        }
    }
}