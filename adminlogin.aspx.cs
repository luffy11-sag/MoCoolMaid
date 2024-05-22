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
    public partial class adminlogin : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["hoid"] != null || Session["hkid"] != null || Session["adminun"] != null)
            {
                Response.Redirect("~/home.aspx");
            }
        }

        protected void btnAdminLogin_Click(object sender, EventArgs e)
        {
            string username = adminLogin.Email;
            string password = adminLogin.Password;

            SqlConnection con = new SqlConnection(_conString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblAdmin WHERE Admin_Username = @adun AND Admin_Password = @adpwd";
            cmd.Parameters.AddWithValue("@adun", username);
            cmd.Parameters.AddWithValue("@adpwd", password);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string ReturnUrl = Convert.ToString(Request.QueryString["qs"]);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    Session["adminun"] = dr["Admin_Username"];
                    Session["adminpassword"] = dr["Admin_Password"];
                    Response.Redirect(ReturnUrl);
                }
                else
                {
                    Session["adminun"] = dr["Admin_Username"];
                    Session["adminpassword"] = dr["Admin_Password"];
                    Response.Redirect("~/Admin/dashboard.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "admModal();", true);
                lblmsgAD.Style.Add("margin-left", "10%");
                lblmsgAD.ForeColor = System.Drawing.Color.Red;
                username = "";
                password = "";
                lblmsgAD.Text = "Admin not registered!";
            }
            con.Close();
        }
    }
}