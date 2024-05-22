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
    public partial class resetPassword : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["userid"]);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser WHERE User_ID = @uid AND User_Password = @pwd";
            cmd.Parameters.AddWithValue("@uid", user_id);
            cmd.Parameters.AddWithValue("@pwd", Encrypt(Password.Text));
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblMsg.Text = "Please Enter New Password or Cancel!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            } else
            {
                dr.Close();
                dr.Close();
                SqlCommand scmd = new SqlCommand();
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "UPDATE tblUser SET User_Password = @pwd WHERE User_ID = @uid";
                scmd.Parameters.AddWithValue("@uid", user_id);
                scmd.Parameters.AddWithValue("@pwd", Encrypt(Password.Text));
                IsUpdated = scmd.ExecuteNonQuery() > 0;
                con.Close();
                if (IsUpdated)
                {
                    Response.Redirect("~/userInfo.aspx");
                }
                else
                {
                    lblMsg.Text = "Error while Resetting Password!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/userInfo.aspx");
        }
    }
}