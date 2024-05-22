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
    public partial class editUsername : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["userid"]);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tblUser SET User_FName = @fname, User_LName = @lname WHERE User_ID = @uid";
            cmd.Parameters.AddWithValue("@uid", user_id);
            cmd.Parameters.AddWithValue("@fname", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@lname", txtLastName.Text);
            con.Open();

            IsUpdated = cmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                Response.Redirect("~/userInfo.aspx");
            }
            else
            {
                lblMsg.Text = "Error while Updating User Name!";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/userInfo.aspx");
        }
    }
}