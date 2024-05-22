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
using System.Security.Cryptography;
using System.Text;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;

namespace MoCoolMaid
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                if (Request.Cookies["houseowneremail"] != null && Request.Cookies["houseownerpassword"] != null)
                {                   
                    hoLogin.Email = Request.Cookies["houseowneremail"].Value;
                    hoLogin.Password = Request.Cookies["houseownerpassword"].Value;
                }

                if (Request.Cookies["housekeeperemail"] != null && Request.Cookies["housekeeperpassword"] != null)
                {
                    hkLogin.Email = Request.Cookies["housekeeperemail"].Value;
                    hkLogin.Password = Request.Cookies["housekeeperpassword"].Value;
                }
            }

            if (Session["hoemail"] != null)
            {
                pnllog.Style.Add("Visibility", "Hidden");
                Page.Controls.Remove(pnllog);
                lgregis.CssClass = "nav-item";
                btnlgout.Visible = true;
                pnlHOAction.Visible = true;
                pnlWelcome.Visible = true;
                btnWelcome.Text = Session["hofname"].ToString() + " " + Session["holname"].ToString();
                pnlSignup.Visible = false;
            }

            if (Session["hkemail"] != null)
            {
                pnllog.Style.Add("Visibility", "Hidden");
                Page.Controls.Remove(pnllog);
                lgregis.CssClass = "nav-item";
                btnlgout.Visible = true;
                pnlbrowsejobs.Visible = true;
                pnlBrwoseHO.Visible = true;
                pnlHKAction.Visible = true;
                pnlWelcome.Visible = true;
                btnWelcome.Text = Session["hkfname"].ToString() + " " + Session["hklname"].ToString();
                pnlSignup.Visible = false;
            }

            if (Session["adminun"] != null)
            {
                pnllog.Style.Add("Visibility", "Hidden");
                Page.Controls.Remove(pnllog);
                lgregis.CssClass = "nav-item";
                btnlgout.Visible = true;
                pnlDashboard.Visible = true;
                pnlAdvert.Visible = false;
                pnlSignup.Visible = false;
            }         
        }

        protected void btnHOLogin_Click(object sender, EventArgs e)
        {
            string email = hoLogin.Email;
            string password = hoLogin.Password;
            bool chk = hoLogin.Chk;
            SqlConnection con = new SqlConnection(_conString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser, tblHouseowner WHERE HO_User_ID = User_ID AND User_Email = @email AND User_Password = @pwd";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pwd", Encrypt(password));

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    Response.Cookies["houseowneremail"].Value = email;
                    Response.Cookies["houseownerpassword"].Value = password;

                    if (chk)
                    {
                        Response.Cookies["houseowneremail"].Expires = DateTime.Now.AddDays(100);
                        Response.Cookies["houseownerpassword"].Expires = DateTime.Now.AddDays(100);
                    }
                    else
                    {
                        Response.Cookies["houseowneremail"].Expires = DateTime.Now.AddDays(-100);
                        Response.Cookies["houseownerpassword"].Expires = DateTime.Now.AddDays(-100);
                    }
                }
                int status = Convert.ToInt32(dr["HO_Status"]);
                if (status == 1)
                {
                    string ReturnUrl = Convert.ToString(Request.QueryString["qs"]);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        Session["hoid"] = dr["HO_ID"];
                        Session["hoemail"] = dr["User_Email"];
                        Session["hopassword"] = dr["User_Password"];
                        Session["userid"] = dr["User_ID"];
                        Session["hofname"] = dr["User_FName"];
                        Session["holname"] = dr["User_LName"];
                        Response.Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["hoid"] = dr["HO_ID"];
                        Session["hoemail"] = dr["User_Email"];
                        Session["hopassword"] = dr["User_Password"];
                        Session["userid"] = dr["User_ID"];
                        Session["hofname"] = dr["User_FName"];
                        Session["holname"] = dr["User_LName"];
                        Response.Redirect("~/home.aspx");
                        
                    }                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "houseownerModal();", true);
                    lblmsgHO.Style.Add("margin-left", "10%");
                    lblmsgHO.ForeColor = System.Drawing.Color.Red;
                    email = "";
                    password = "";
                    lblmsgHO.Text = "Your account has been suspended!";
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "houseownerModal();", true);
                lblmsgHO.Style.Add("margin-left", "10%");
                lblmsgHO.ForeColor = System.Drawing.Color.Red;
                email = "";
                password = "";
                lblmsgHO.Text = "Wrong Email or Password!";
            }

        }       

        protected void btnHKLogin_Click(object sender, EventArgs e)
        {
            string email = hkLogin.Email;
            string password = hkLogin.Password;
            bool chk = hkLogin.Chk;
            SqlConnection con = new SqlConnection(_conString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser, tblHousekeeper WHERE HK_User_ID = User_ID AND User_Email = @email AND User_Password = @pwd";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@pwd", Encrypt(password));

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    Response.Cookies["housekeeperemail"].Value = email;
                    Response.Cookies["housekeeperpassword"].Value = password;

                    if (chk)
                    {
                        Response.Cookies["housekeeperemail"].Expires = DateTime.Now.AddDays(100);
                        Response.Cookies["housekeeperpassword"].Expires = DateTime.Now.AddDays(100);
                    }
                    else
                    {
                        Response.Cookies["housekeeperemail"].Expires = DateTime.Now.AddDays(-100);
                        Response.Cookies["housekeeperpassword"].Expires = DateTime.Now.AddDays(-100);
                    }
                }
                int status = Convert.ToInt32(dr["HK_Status"]);
                if (status == 1)
                {
                    string ReturnUrl = Convert.ToString(Request.QueryString["qs"]);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        Session["hkid"] = dr["HK_ID"];
                        Session["hkemail"] = dr["User_Email"];
                        Session["hkpassword"] = dr["User_Password"];
                        Session["userid"] = dr["User_ID"];
                        Session["hkfname"] = dr["User_FName"];
                        Session["hklname"] = dr["User_LName"];
                        Response.Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["hkid"] = dr["HK_ID"];
                        Session["hkemail"] = dr["User_Email"];
                        Session["hkpassword"] = dr["User_Password"];
                        Session["userid"] = dr["User_ID"];
                        Session["hkfname"] = dr["User_FName"];
                        Session["hklname"] = dr["User_LName"];
                        Response.Redirect("~/home.aspx");
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "housekeeperModal();", true);
                    lblmsgHK.Style.Add("margin-left", "10%");
                    lblmsgHK.ForeColor = System.Drawing.Color.Red;
                    email = "";
                    password = "";
                    lblmsgHK.Text = "Your account has been suspended!";
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "housekeeperModal();", true);
                lblmsgHK.Style.Add("margin-left", "10%");
                lblmsgHK.ForeColor = System.Drawing.Color.Red;
                email = "";
                password = "";
                lblmsgHK.Text = "Wrong Email or Password!";
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

        public string Encrypt(string clearText)
        {
            // defining encrytion key
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new
               byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    // encoding using key
                    using (CryptoStream cs = new CryptoStream(ms,
                   encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        void logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/home.aspx");
        }

        protected void btnlgout_Click(object sender, EventArgs e)
        {
            logout();
        }

        protected void btnWelcome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/userInfo.aspx");
        }
    }
}
