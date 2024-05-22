using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.HouseOwner
{
    public partial class ratehk : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["hoemail"])))
            {
                Response.Redirect("~/houseownerlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            BindHousekeeper();
        }

        private void BindHousekeeper()
        {
            int ho_id = Convert.ToInt32(Session["hoid"]);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ccmd = con.CreateCommand();
            ccmd.CommandType = CommandType.Text;
            ccmd.CommandText = "SELECT u.User_FName AS fname, ";
            ccmd.CommandText += "u.User_LName AS lname, ";
            ccmd.CommandText += "u.User_Email AS email, ";
            ccmd.CommandText += "hk.HK_PP AS image, ";
            ccmd.CommandText += "hk.HK_Status AS hkstatus, ";
            ccmd.CommandText += "hk.HK_ID AS HKID, ";
            ccmd.CommandText += "ja.Job_ID AS jobid, ";
            ccmd.CommandText += "ja.Application_Date AS appdate, ";
            ccmd.CommandText += "ja.Application_Status AS tumstatus, ";
            ccmd.CommandText += "cat.JCategory_Name AS jname, ";
            ccmd.CommandText += "j.Job_Status AS jstatus, ";
            ccmd.CommandText += "j.Job_ID AS JobId ";
            ccmd.CommandText += "FROM tblUser u, tblHousekeeper hk, tblJobApplication ja, tblHouseowner ho, tblJob j, tblJobCategory cat ";
            ccmd.CommandText += "WHERE u.User_ID = hk.HK_User_ID ";
            ccmd.CommandText += "AND ja.Job_ID = j.Job_ID ";
            ccmd.CommandText += "AND ja.HK_ID = hk.HK_ID ";
            ccmd.CommandText += "AND j.JCategory_ID = cat.JCategory_ID ";
            ccmd.CommandText += "AND j.HO_ID = ho.HO_ID ";
            ccmd.CommandText += "AND hk.HK_Status = '1' ";
            ccmd.CommandText += "AND ho.HO_ID = @hoid ";
            ccmd.CommandText += "AND ja.Application_Status = '1' ";
            ccmd.Parameters.AddWithValue("@hoid", ho_id);
            SqlDataAdapter sqlda = new SqlDataAdapter(ccmd);
            DataTable dta = new DataTable();
            sqlda.Fill(dta);
            con.Close();
            gvs.DataSource = dta;
            gvs.DataBind();
        }

        protected void lbtnRate_Click(object sender, EventArgs e)
        {
            LinkButton lnkRate = (LinkButton)sender;

            string hk_id = lnkRate.CommandArgument;

            if (chkexist(Convert.ToInt32(hk_id)))
            {
                lblMsg.Text = "You have already rated this housekeeper!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Response.Redirect($"rating.aspx?hk_ID={hk_id}");
            }          
        }

        private Boolean chkexist(int x)
        {
            SqlConnection con = new SqlConnection(_conString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * FROM tblRating WHERE HK_ID = @hkid and HO_ID = @hoid";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@hoid", Session["hoid"]);
            cmd.Parameters.AddWithValue("@hkid", x);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}