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
    public partial class rating : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["hk_id"]) && int.TryParse(Request.QueryString["hk_id"], out int hk_id))
            {
                int rating = Convert.ToInt32(Rating1.CurrentRating);
                String review = txtReview.Text.Trim();
                int ho_id = Convert.ToInt32(Session["hoid"]);

                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO tblRating(Rating_Num, Review, Rating_Date, HO_ID, HK_ID) VALUES(@rating, @review, @ratingdate, @hoid, @hkid)";
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@review", review);
                cmd.Parameters.AddWithValue("@hoid", ho_id);
                cmd.Parameters.AddWithValue("@hkid", hk_id);
                cmd.Parameters.AddWithValue("@ratingdate", DateTime.Now);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Redirect("~/HouseOwner/ratehk.aspx");
            }
        }
    }
}