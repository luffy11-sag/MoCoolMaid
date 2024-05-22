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
            if (string.IsNullOrEmpty(Convert.ToString(Session["hoemail"])))
            {
                Response.Redirect("~/houseownerlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            if (!IsPostBack)
            {
                BindLocation();
                ListItem defCategory = new ListItem("Select Location", "-1");
                ddlDistrict.Items.Insert(0, defCategory);
                
            }
            txtSearch_TextChanged(sender, null);
        }

        protected void lvHK_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkViewProfile_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewProfileJob = (LinkButton)sender;
            string hk_id = lnkViewProfileJob.CommandArgument;

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHousekeeper hk, tblUser u, tblLocation l WHERE hk.HK_User_ID = u.User_ID AND u.User_City = l.Loc_ID AND hk.HK_ID = @hkid";
            cmd.Parameters.AddWithValue("@hkid", hk_id);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            

            while (dr.Read())
            {
                string lastname = dr["User_LName"].ToString();
                string firstname = dr["User_FName"].ToString();
                string email = dr["User_Email"].ToString();
                string location = dr["District"].ToString();
                string phone = dr["User_Phone"].ToString();

                profilePic.ImageUrl = ResolveUrl("~/images/" + dr["HK_PP"].ToString());
                lblLastName.Text = $"<strong>Last Name:</strong> {lastname}";
                lblFirstName.Text = $"<strong>First Name:</strong> {firstname}";
                lblEmail.Text = $"<strong>Email:</strong> {email}";
                lblLocation.Text = $"<strong>Location:</strong> {location}";
                lblPhone.Text = $"<strong>Phone:</strong> {phone}";
                cvLink.HRef = ResolveUrl("~/cv/" + dr["HK_CV"].ToString());
                hfUserID.Value = dr["HK_ID"].ToString();
            }
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "viewHKModal();", true);
        }

        protected void lvHK_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            txtSearch_TextChanged(sender, null);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String strhkname = txtSearch.Text.Trim();
            String strDistrict = ddlDistrict.SelectedValue;
            String strGender = ddlGender.SelectedValue;

            String sqlhkname = "";
            String sqlDistrict = "";
            String sqlGender = "";

            if (strhkname != null)
            {
                sqlhkname = " AND (u.User_FName LIKE @name + '%' OR u.User_LName LIKE @name + '%')";
            }
            if (strDistrict != "-1")
            {
                sqlDistrict = " AND u.User_City = @districts";
            }
            if (strGender != "-1")
            {
                if (ddlGender.SelectedValue == "1")
                {
                    sqlGender = " AND u.User_Gender = 'Mr'";
                }
                else if (ddlGender.SelectedValue == "2")
                {
                    sqlGender = " AND u.User_Gender = 'Mrs'";
                }
            }

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHousekeeper hk, tblUser u, tblLocation l WHERE hk.HK_User_ID = u.User_ID AND u.User_city = l.Loc_ID AND hk.HK_Status = 1" + sqlhkname + sqlDistrict + sqlGender;
            cmd.Parameters.AddWithValue("@name", strhkname);
            cmd.Parameters.AddWithValue("@districts", strDistrict);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHK = new DataTable();

            using (da)
            {
                da.Fill(dtHK);
            }

            lvHK.DataSource = dtHK;
            lvHK.DataBind();
        }

        private void BindLocation()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblLocation";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtLocation = new DataTable();

            using (da)
            {
                da.Fill(dtLocation);
            }

            ddlDistrict.DataSource = dtLocation;
            ddlDistrict.DataTextField = "District";
            ddlDistrict.DataValueField = "Loc_ID";
            ddlDistrict.DataBind();
        }

        protected void lbtnViewRating_Click(object sender, EventArgs e)
        {
            string hk_id = hfUserID.Value;

            Response.Redirect($"~/viewRatings.aspx?hk_ID={hk_id}");
        }
    }
}