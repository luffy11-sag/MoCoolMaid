using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace MoCoolMaid
{
    public partial class housekeeperlogin : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["hoid"] != null || Session["hkid"] != null || Session["adminun"] != null)
            {
                Response.Redirect("~/home.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["housekeeperemail"] != null && Request.Cookies["housekeeperpassword"] != null)
                {
                    hkLogin1.Email = Request.Cookies["housekeeperemail"].Value;
                    hkLogin1.Password = Request.Cookies["housekeeperpassword"].Value;
                }
            }
        }

        protected void btnHKLogin_Click(object sender, EventArgs e)
        {
            string email = hkLogin1.Email;
            string password = hkLogin1.Password;
            bool chk = hkLogin1.Chk;
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
                lblmsgHK.Style.Add("margin-left", "10%");
                lblmsgHK.ForeColor = System.Drawing.Color.Red;
                email = "";
                password = "";
                lblmsgHK.Text = "Wrong Email or Password!";
            }
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
    }
}