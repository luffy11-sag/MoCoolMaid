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
                BindLocation();
                ListItem defCategory = new ListItem("Select Location", "-1");
                ddlDistrict.Items.Insert(0, defCategory);
            }
            txtSearch_TextChanged(sender, null);
        }

        private void BindHouseOwnerData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHouseowner ho, tblUser u, tblLocation l WHERE ho.HO_User_ID = u.User_ID AND u.User_City = l.Loc_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHO = new DataTable();

            using (da)
            {
                da.Fill(dtHO);
            }

            lvHO.DataSource = dtHO;
            lvHO.DataBind();
        }

        protected void lvHO_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            txtSearch_TextChanged(sender, null);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String strhoname = txtSearch.Text.Trim();
            String strDistrict = ddlDistrict.SelectedValue;
            String strGender = ddlGender.SelectedValue;

            String sqlname = "";
            String sqlDistrict = "";
            String sqlGender = "";

            if (strhoname != null)
            {
                sqlname = " AND (u.User_FName LIKE @name + '%' OR u.User_LName LIKE @name + '%')";
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
            cmd.CommandText = "SELECT * FROM tblHouseowner ho, tblUser u, tblLocation l WHERE ho.HO_User_ID = u.User_ID AND u.User_City = l.Loc_ID AND ho.HO_Status = 1" + sqlname + sqlDistrict + sqlGender;
            cmd.Parameters.AddWithValue("@name", strhoname);
            cmd.Parameters.AddWithValue("@districts", strDistrict);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHO = new DataTable();

            using (da)
            {
                da.Fill(dtHO);
            }

            lvHO.DataSource = dtHO;
            lvHO.DataBind();
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
    }
}