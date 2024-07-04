using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt;
namespace MoCoolMaid
{
    public partial class signup : System.Web.UI.Page
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
                fuSection.Visible = false;
                bindDistrict();

                ListItem defDistrict = new ListItem("Select District", "-1");
                ddlDistrict.Items.Insert(0, defDistrict);
            }

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue == "2") {
                fuSection.Visible = true;
            } else
            {
                fuSection.Visible = false;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            string filecv = "";
            string filepp = "";
            string strDate = txtDateOfBirth.Text;
            DateTime dt = Convert.ToDateTime(strDate);
            if (ddlRole.SelectedValue == "2")
            {
                
                if (fuCV.HasFile)
                {
                    if (CheckFileTypeCV(fuCV.FileName))
                    {
                        filecv = Path.GetFileName(fuCV.PostedFile.FileName);
                        fuCV.PostedFile.SaveAs(Server.MapPath("~/cv/") + filecv);
                    } else
                    {
                        lblTxtMessage.Text = "Wrong file extension for CV. Upload pdf or docx document!";
                        lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                } else
                {
                    lblTxtMessage.Text = "Please upload a CV or another relevant document!";
                    lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (fuPP.HasFile)
                {
                    if (CheckFileTypePP(fuPP.FileName))
                    {
                        filepp = Path.GetFileName(fuPP.PostedFile.FileName);
                        fuPP.PostedFile.SaveAs(Server.MapPath("~/images/") + filepp);
                    } else
                    {
                        lblTxtMessage.Text = "Wrong file extension for profile picture!";
                        lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                } else
                {
                    lblTxtMessage.Text = "Please upload a profile picture!";
                    lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser WHERE User_Email = @email";
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if ( dr.HasRows)
            {
                lblTxtMessage.Text = "Email was already used to register, Please Choose Another";
                lblTxtMessage.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
            }
            else if (!chkDate(dt))
            {
                lblTxtMessage.Text = "Invalid Date of Birth. Must be 18 or higher to sign up!";
            }
            else
            {
                dr.Close();
                
                SqlCommand scmd = new SqlCommand();
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "INSERT INTO tblUser(User_LName, User_FName, User_DOB, User_Gender, User_Email, User_Password, User_City, User_Phone) VALUES (@lname, @fname, @dob, @gender, @uemail, @pwd, @city, @phone)";
                scmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim());
                scmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim());
                scmd.Parameters.AddWithValue("@dob", dt);
                scmd.Parameters.AddWithValue("@gender", rblGender.Text);
                scmd.Parameters.AddWithValue("@uemail", txtEmail.Text.Trim());

                //here we have used the function HashPassword to hash the password.
                scmd.Parameters.AddWithValue("@pwd", HashPassword(txtPassword.Text));
                scmd.Parameters.AddWithValue("@city", ddlDistrict.SelectedValue);
                scmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());               
                scmd.ExecuteNonQuery();

                scmd.CommandText = "SELECT SCOPE_IDENTITY()";
                int userID = Convert.ToInt32(cmd.ExecuteScalar());
                
                if (ddlRole.SelectedValue == "2")
                {
                    dr.Close();
                    SqlCommand hkcmd = new SqlCommand();
                    hkcmd.Connection = con;
                    hkcmd.CommandType = CommandType.Text;
                    hkcmd.CommandText = "INSERT INTO tblHousekeeper(HK_CV, HK_PP, HK_Status, HK_User_ID) VALUES (@cv, @pp, @status, @uid)";
                    hkcmd.Parameters.AddWithValue("@cv", filecv);
                    hkcmd.Parameters.AddWithValue("@pp", filepp);
                    hkcmd.Parameters.AddWithValue("@status", 1);
                    hkcmd.Parameters.AddWithValue("@uid", userID);
                    hkcmd.ExecuteNonQuery();
                } else
                {
                    SqlCommand hocmd = new SqlCommand();
                    hocmd.Connection = con;
                    hocmd.CommandType = CommandType.Text;
                    hocmd.CommandText = "INSERT INTO tblHouseowner(HO_Status, HO_User_ID) VALUES (@status, @uid)";
                    hocmd.Parameters.AddWithValue("@status", 1);
                    hocmd.Parameters.AddWithValue("@uid", userID);
                    hocmd.ExecuteNonQuery();
                }
                con.Close();
                Response.Redirect("~/home.aspx");
            }
        }


          //method to hash the password
   public string HashPassword(string password)
   {
   string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());

   return hashedPassword;
   }



        bool CheckFileTypeCV(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".pdf":
                    return true;
                case ".docx":
                    return true;               
                default:
                    return false;
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

       /* public string Encrypt(string clearText)
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
*/

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void bindDistrict()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblLocation";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtDistrict = new DataTable();
            using (da)
            {
                da.Fill(dtDistrict);
            }
            ddlDistrict.DataSource = dtDistrict;
            ddlDistrict.DataTextField = "District";
            ddlDistrict.DataValueField = "Loc_ID";
            ddlDistrict.DataBind();
        }

        private Boolean chkDate(DateTime date)
        {
            DateTime currentDate = DateTime.Now;
            DateTime eighteenYearsAgo = currentDate.AddYears(-18);
            if (date >= eighteenYearsAgo)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
