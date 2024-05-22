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

namespace MoCoolMaid
{
    public partial class houseownerlogin : System.Web.UI.Page
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
                if (Request.Cookies["houseowneremail"] != null && Request.Cookies["houseownerpassword"] != null)
                {
                    hoLogin.Email = Request.Cookies["houseowneremail"].Value;
                    hoLogin.Password = Request.Cookies["houseownerpassword"].Value;
                }
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
                lblmsgHO.Style.Add("margin-left", "10%");
                lblmsgHO.ForeColor = System.Drawing.Color.Red;
                email = "";
                password = "";
                lblmsgHO.Text = "Wrong Email or Password!";
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