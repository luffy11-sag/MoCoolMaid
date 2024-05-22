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
    public partial class viewRatings : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRating();
        }

        private void BindRating()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["hk_id"]) && int.TryParse(Request.QueryString["hk_id"], out int hk_id))
            {
                SqlConnection con = new SqlConnection(_conString);
                con.Open();
                SqlCommand ccmd = con.CreateCommand();
                ccmd.CommandType = CommandType.Text;
                ccmd.CommandText = "SELECT r.*, hk.*, ho.*, u.User_LName AS HkLastName, u.User_FName AS HkFirstName, hoUser.User_LName AS HoLastName, hoUser.User_FName AS HoFirstName " +
                   "FROM tblRating r, tblHousekeeper hk, tblHouseowner ho, tblUser u, tblUser hoUser " +
                   "WHERE r.HK_ID = hk.HK_ID " +
                   "AND hk.HK_User_ID = u.User_ID " +
                   "AND r.HO_ID = ho.HO_ID " +
                   "AND ho.HO_User_ID = hoUser.User_ID " +
                   "AND r.HK_ID = @hkid";


                ccmd.Parameters.AddWithValue("@hkid", hk_id);
                SqlDataAdapter sqlda = new SqlDataAdapter(ccmd);
                DataTable dta = new DataTable();
                sqlda.Fill(dta);
                con.Close();
                lvComments.DataSource = dta;
                lvComments.DataBind();
                if (dta.Rows.Count > 0)
                {
                    DataRow row = dta.Rows[0];

                    
                    string hkFirstName = row["HkFirstName"].ToString();
                    string hkLastName = row["HkLastName"].ToString();

                    lblHK.Text = $"Ratings For: {hkFirstName} {hkLastName}";
                }               
            }
        }
    }
}