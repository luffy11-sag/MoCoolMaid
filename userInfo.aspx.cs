using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.IO;

namespace MoCoolMaid
{
    public partial class userInfo : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {   
                BindLocation();
                ListItem defCategory = new ListItem("Select Location", "-1");
                ddlLocation.Items.Insert(0, defCategory);

                if (Session["hkemail"] != null)
                {
                    pnlCV.Visible = true;
                    pnlProfilePic.Visible = true;
                    bindHkData();
                }

                if (Session["hoemail"] != null)
                {
                    pnlCV.Visible = false;
                    pnlProfilePic.Visible = false;
                    bindHoData();
                }
            }
        }

        private void bindHkData()
        {
            int hk_id = Convert.ToInt32(Session["hkid"]);

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
                lblUsername.Text = firstname + " " + lastname;
                lblEmail.Text = email;
                lblLocation.Text = location;
                lblPhone.Text = phone;
                cvLink.HRef = ResolveUrl("~/cv/" + dr["HK_CV"].ToString());
            }
            con.Close();
        }

        private void bindHoData()
        {
            int ho_id = Convert.ToInt32(Session["hoid"]);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHouseowner ho, tblUser u, tblLocation l WHERE ho.HO_User_ID = u.User_ID AND u.User_City = l.Loc_ID AND ho.HO_ID = @hoid";
            cmd.Parameters.AddWithValue("@hoid", ho_id);
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

                lblUsername.Text = firstname + " " + lastname;
                lblEmail.Text = email;
                lblLocation.Text = location;
                lblPhone.Text = phone;
            }
            con.Close();
        }

        protected void resetPasswordBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/resetPassword.aspx");
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

            ddlLocation.DataSource = dtLocation;
            ddlLocation.DataTextField = "District";
            ddlLocation.DataValueField = "Loc_ID";
            ddlLocation.DataBind();
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

        bool CheckFileTypeCV(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".docx":
                    return true;
                case ".doc":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }

        protected void btnUpdatePic_Click(object sender, EventArgs e)
        {
            if (Session["hkid"] != null)
            {
                int hk_id = Convert.ToInt32(Session["hkid"]);
                string filepp = "";
                Boolean IsUpdated = false;
                if (fuPP.HasFile)
                {
                    if (CheckFileTypePP(fuPP.FileName))
                    {
                        filepp = Path.GetFileName(fuPP.PostedFile.FileName);
                        fuPP.PostedFile.SaveAs(Server.MapPath("~/images/") + filepp);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editPicModal();", true);
                        lblUpdatePicMsg.Text = "Wrong file extension for profile picture!";
                        lblUpdatePicMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editPicModal();", true);
                    lblUpdatePicMsg.Text = "Please upload a profile picture!";
                    lblUpdatePicMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE tblHousekeeper SET HK_PP = @fuPP WHERE HK_ID = @hkid";
                cmd.Parameters.AddWithValue("@fuPP", filepp);
                cmd.Parameters.AddWithValue("@hkid", hk_id);
                con.Open();

                IsUpdated = cmd.ExecuteNonQuery() > 0;

                con.Close();
                if (IsUpdated)
                {
                    bindHkData();
                }
                else
                {
                    lblUpdatePicMsg.Text = "Error while Updating Profile Picture!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editPicModal();", true);
                }
            }
        }

        protected void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["userid"]);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand scmd = new SqlCommand();
            scmd.CommandType = CommandType.Text;

            scmd.CommandText = "UPDATE tblUser SET User_city = @District WHERE User_ID = @uid";
            scmd.Parameters.AddWithValue("@uid", user_id);
            scmd.Parameters.AddWithValue("@District", ddlLocation.SelectedValue);
            scmd.Connection = con;
            con.Open();

            IsUpdated = scmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                if (Session["hoid"] != null)
                {
                    bindHoData();
                }
                else if (Session["hkid"] != null) {
                    bindHkData();
                }
            }
            else
            {
                lblMsg.Text = "Error while updating Location!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editLocationModal();", true);
            }
        }

        protected void btnUpdatePhone_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["userid"]);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand scmd = new SqlCommand();
            scmd.CommandType = CommandType.Text;

            scmd.CommandText = "UPDATE tblUser SET User_Phone = @phone WHERE User_ID = @uid";
            scmd.Parameters.AddWithValue("@uid", user_id);
            scmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            scmd.Connection = con;
            con.Open();

            IsUpdated = scmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                if (Session["hoid"] != null)
                {
                    bindHoData();
                }
                else if (Session["hkid"] != null)
                {
                    bindHkData();
                }
            }
            else
            {
                lblMsg.Text = "Error while updating Phone Number!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editLocationModal();", true);
            }
        }

        protected void btnUpdateCV_Click(object sender, EventArgs e)
        {
            if (Session["hkid"] != null)
            {
                int hk_id = Convert.ToInt32(Session["hkid"]);
                string filecv = "";
                Boolean IsUpdated = false;
                if (fuCV.HasFile)
                {
                    if (CheckFileTypeCV(fuCV.FileName))
                    {
                        filecv = Path.GetFileName(fuCV.PostedFile.FileName);
                        fuCV.PostedFile.SaveAs(Server.MapPath("~/cv/") + filecv);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editCVModal();", true);
                        lblmsgCV.Text = "Wrong file extension for CV!";
                        lblmsgCV.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editCVModal();", true);
                    lblmsgCV.Text = "Please upload document!";
                    lblmsgCV.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE tblHousekeeper SET HK_CV = @fuCV WHERE HK_ID = @hkid";
                cmd.Parameters.AddWithValue("@fuCV", filecv);
                cmd.Parameters.AddWithValue("@hkid", hk_id);
                con.Open();

                IsUpdated = cmd.ExecuteNonQuery() > 0;

                con.Close();
                if (IsUpdated)
                {
                    bindHkData();
                }
                else
                {
                    lblmsgCV.Text = "Error while Updating CV!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "editCVModal();", true);
                }
            }
        }
    }
}