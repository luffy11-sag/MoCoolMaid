using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Drawing;

namespace MoCoolMaid
{
    public partial class testimonials : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["hoid"] == null && Session["hkid"] == null)
            {
                Response.Redirect("~/home.aspx");
            }
        }

        bool CheckFileTypePP(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            string filepp = "";
            Boolean IsAdded = false;
            int uid = 0;
            if (fuPP.HasFile)
            {
                if (CheckFileTypePP(fuPP.FileName))
                {
                    filepp = Path.GetFileName(fuPP.PostedFile.FileName);
                    fuPP.PostedFile.SaveAs(Server.MapPath("~/images/testimonial/") + filepp);
                }
                else
                {
                    lblTxtMessage.Text = "Wrong file extension for profile picture!";
                    lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }
            else
            {
                lblTxtMessage.Text = "Please upload a profile picture!";
                lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (Session["hoid"] != null)
            {
                int ho_id = Convert.ToInt32(Session["hoid"]);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT u.User_ID FROM tblUser u, tblHouseowner ho WHERE u.User_ID = ho.HO_User_ID AND ho.HO_ID = @hoid";
                cmd.Parameters.AddWithValue("@hoid", ho_id);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uid = Convert.ToInt32(dr["User_ID"]);
                }

                dr.Close();
            }
            if (Session["hkid"] != null)
            {
                int hk_id = Convert.ToInt32(Session["hkid"]);               
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT u.User_ID FROM tblUser u, tblHousekeeper hk WHERE u.User_ID = hk.HK_User_ID AND hk.HK_ID = @hkid";
                cmd.Parameters.AddWithValue("@hkid", hk_id);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    uid = Convert.ToInt32(dr["User_ID"]);
                }

                dr.Close();
            }

            if (chkexist(uid))
            {
                lblTxtMessage.Text = "You Already Wrote a Testimonial!";
                lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "INSERT INTO tblTestimonial(Testimonial_Desc, Testimonial_Date, Testimonial_PP, User_ID) Values(@desc, @date, @pp, @uid)";
                cmd2.Parameters.AddWithValue("@desc", txtTestimonial.Text.Trim());
                cmd2.Parameters.AddWithValue("@date", DateTime.Now);
                cmd2.Parameters.AddWithValue("@pp", filepp);
                cmd2.Parameters.AddWithValue("@uid", uid);
                IsAdded = cmd2.ExecuteNonQuery() > 0;
                con.Close();
                if (IsAdded)
                {
                    Response.Redirect("~/home.aspx#testimonials");
                }
                else
                {
                    lblTxtMessage.Text = "An Error Occured";
                    lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private Boolean chkexist(int x)
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //search for user
            cmd.CommandText = "SELECT * FROM tblTestimonial WHERE User_ID = @uid";
            cmd.Connection = con;
            //create a parameterized query           
            cmd.Parameters.AddWithValue("@uid", x);
            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            //Check if user subscription already exists in the table
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