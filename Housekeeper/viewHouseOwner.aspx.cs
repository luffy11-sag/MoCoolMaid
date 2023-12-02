using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace MoCoolMaid.Housekeeper
{
    public partial class viewHouseOwner : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindHouseOwnerData();
            }
        }

        private void BindHouseOwnerData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHouseowner ho, tblUser u WHERE ho.HO_User_ID = u.User_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHO = new DataTable();

            using (da)
            {
                da.Fill(dtHO);
            }

            lvHO.DataSource = dtHO;
            lvHO.DataBind();
        }
    }
}