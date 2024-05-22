using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MoCoolMaid.HouseOwner
{
    public partial class approveApplication : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["hoemail"])))
            {
                Response.Redirect("~/houseownerlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            getApplicationDetails();
        }

        void getApplicationDetails()
        {
            int ho_id = Convert.ToInt32(Session["hoid"]);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ccmd = con.CreateCommand();
            ccmd.CommandType = CommandType.Text;
            ccmd.CommandText = "SELECT u.User_FName AS fname, ";
            ccmd.CommandText += "u.User_LName AS lname, ";
            ccmd.CommandText += "u.User_Email AS email, ";
            ccmd.CommandText += "hk.HK_PP AS image, ";
            ccmd.CommandText += "hk.HK_Status AS hkstatus, ";
            ccmd.CommandText += "hk.HK_ID AS HKID, ";
            ccmd.CommandText += "ja.Job_ID AS jobid, ";
            ccmd.CommandText += "ja.Application_Date AS appdate, ";
            ccmd.CommandText += "ja.Application_Status AS tumstatus, ";
            ccmd.CommandText += "cat.JCategory_Name AS jname, ";
            ccmd.CommandText += "j.Job_Status AS jstatus, ";
            ccmd.CommandText += "j.Job_ID AS JobId ";
            ccmd.CommandText += "FROM tblUser u, tblHousekeeper hk, tblJobApplication ja, tblHouseowner ho, tblJob j, tblJobCategory cat ";
            ccmd.CommandText += "WHERE u.User_ID = hk.HK_User_ID ";
            ccmd.CommandText += "AND ja.Job_ID = j.Job_ID ";
            ccmd.CommandText += "AND ja.HK_ID = hk.HK_ID ";
            ccmd.CommandText += "AND j.JCategory_ID = cat.JCategory_ID ";
            ccmd.CommandText += "AND j.HO_ID = ho.HO_ID ";
            ccmd.CommandText += "AND hk.HK_Status = '1' ";
            ccmd.CommandText += "AND ho.HO_ID = @hoid ";
            ccmd.Parameters.AddWithValue("@hoid", ho_id);
            SqlDataAdapter sqlda = new SqlDataAdapter(ccmd);
            DataTable dta = new DataTable();
            sqlda.Fill(dta);
            con.Close();
            gvs.DataSource = dta;
            gvs.DataBind();
        }

        protected void gvs_PreRender1(object sender, EventArgs e)
        {
            if (gvs.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }

        }

        protected void lnkdeny_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grvRow = (GridViewRow)lnk.NamingContainer;
            HiddenField hf = grvRow.FindControl("hidjob") as HiddenField;
            int job_id = Convert.ToInt32(hf.Value);
            int hk_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ucmd = con.CreateCommand();
            ucmd.CommandType = CommandType.Text;
            ucmd.CommandText = "UPDATE tblJobApplication SET Application_Status = '2' Where HK_ID ='" + hk_id + "' and Job_ID ='" + job_id + "'";
            ucmd.ExecuteNonQuery();
            con.Close();
            getApplicationDetails();
        }

        protected void lnkgrant_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grvRow = (GridViewRow)lnk.NamingContainer;
            HiddenField hf = grvRow.FindControl("hidjob") as HiddenField;
            int job_id = Convert.ToInt32(hf.Value);
            int hk_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ucmd = con.CreateCommand();
            ucmd.CommandType = CommandType.Text;
            ucmd.CommandText = "UPDATE tblJobApplication SET Application_Status = '1' Where HK_ID ='" + hk_id + "' and Job_ID ='" + job_id + "'";
            ucmd.ExecuteNonQuery();
            con.Close();
            getApplicationDetails();
            sendemail(hk_id);
        }

        protected void gvs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming that "tumstatus" is the data field for access status
                int accessStatus = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "tumstatus"));

                // Find the cell with the access status
                TableCell statusCell = e.Row.Cells[6]; // Assuming the index of the "tumstatus" column is 6, adjust accordingly

                // Replace the numeric value with user-friendly text
                switch (accessStatus)
                {
                    case 0:
                        statusCell.Text = "Pending";
                        break;
                    case 1:
                        statusCell.Text = "Accepted";
                        break;
                    case 2:
                        statusCell.Text = "Rejected";
                        break;
                    // Add more cases if needed
                    default:
                        // Handle unexpected values
                        break;
                }
            }
        }

        private void sendemail(int hk_id)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHousekeeper hk, tblUser u WHERE u.User_ID = hk.HK_User_ID AND hk.HK_ID = @hkid";
            cmd.Parameters.AddWithValue("@hkid", hk_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtHK = new DataTable();

            using (da)
            {
                da.Fill(dtHK);

                if (dtHK.Rows.Count > 0)
                {
                    // Access the email and username from the first row (assuming they are present in the result)
                    string hk_email = dtHK.Rows[0]["User_Email"].ToString();
                    string hk_username = dtHK.Rows[0]["User_FName"].ToString() + " " + dtHK.Rows[0]["User_LName"].ToString();

                    txtEmail.Text = hk_email;
                    txtUsername.Text = hk_username;
                }
            }
            SmtpSection smtpSection =
           (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, txtEmail.Text.Trim()))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Application at MoCoolMaid";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + txtUsername.Text + ", your application for a job was accepted, check it out.");
                    //msgBody.Append(txtbody.Text.Trim());
                    msgBody.Append("<a href='https://" +
                    HttpContext.Current.Request.Url.Authority + "/Housekeeper/appliedjobs'>Click here view applied jobs...</ a > ");


                    m.Body = msgBody.ToString();
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new
                    NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);
                    Response.Write("Email Sent successfully");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}
