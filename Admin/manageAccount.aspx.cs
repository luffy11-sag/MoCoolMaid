using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using AjaxControlToolkit;

namespace MoCoolMaid.Admin
{
    public partial class manageAccount : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["adminun"])))
            {
                Response.Redirect("~/adminlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }
            if (!IsPostBack)
            {
                BindUserData();
            }
        }

        private void BindHousekeeperData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHousekeeper hk, tblUser u, tblLocation l WHERE hk.HK_User_ID = u.User_ID AND u.User_city = l.Loc_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dthk = new DataTable();
            using (da)
            {
                da.Fill(dthk);
            }

            gvsUsers.DataSource = dthk;
            gvsUsers.DataBind();

            foreach (GridViewRow row in gvsUsers.Rows)
            {
                LinkButton lbtnDeactivate = (LinkButton)row.FindControl("lbtnDeactivate1");
                LinkButton lbtnActivate = (LinkButton)row.FindControl("lbtnActivate1");

                if (lbtnDeactivate != null && lbtnActivate != null)
                {
                    DataRowView dataRowView = (DataRowView)row.DataItem;
                    if (dataRowView != null)
                    {
                        bool hkStatus = dataRowView.Row.Table.Columns.Contains("HK_Status") &&
                                        dataRowView["HK_Status"] != DBNull.Value &&
                                        Convert.ToBoolean(dataRowView["HK_Status"]);

                        lbtnDeactivate.Visible = ddlAccount.SelectedValue == "1" && hkStatus;
                        lbtnActivate.Visible = ddlAccount.SelectedValue == "1" && !hkStatus;
                    }
                }
            }
        }

        private void BindHouseOwnerData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHouseowner ho, tblUser u, tblLocation l WHERE ho.HO_User_ID = u.User_ID AND u.User_city = l.Loc_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtho = new DataTable();
            using (da)
            {
                da.Fill(dtho);
            }

            gvsUsers.DataSource = dtho;
            gvsUsers.DataBind();

            foreach (GridViewRow row in gvsUsers.Rows)
            {
                LinkButton lbtnDeactivate = (LinkButton)row.FindControl("lbtnDeactivate1");
                LinkButton lbtnActivate = (LinkButton)row.FindControl("lbtnActivate1");

                if (lbtnDeactivate != null && lbtnActivate != null)
                {
                    DataRowView dataRowView = (DataRowView)row.DataItem;
                    if (dataRowView != null)
                    {
                        bool hoStatus = dataRowView.Row.Table.Columns.Contains("HO_Status") &&
                                        dataRowView["HO_Status"] != DBNull.Value &&
                                        Convert.ToBoolean(dataRowView["HO_Status"]);

                        lbtnDeactivate.Visible = ddlAccount.SelectedValue == "2" && hoStatus;
                        lbtnActivate.Visible = ddlAccount.SelectedValue == "2" && !hoStatus;
                    }
                }
            }
        }




        private void BindUserData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"SELECT u.*, hk.HK_Status AS HKStatus, ho.HO_Status AS HOStatus
                FROM tblUser u
                LEFT JOIN tblHousekeeper hk ON u.User_ID = hk.HK_User_ID
                LEFT JOIN tblHouseowner ho ON u.User_ID = ho.HO_User_ID";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            using (da)
            {
                da.Fill(dt);
            }

            gvs.DataSource = dt;
            gvs.DataBind();

            foreach (GridViewRow row in gvs.Rows)
            {
                LinkButton lbtnDeactivate = (LinkButton)row.FindControl("lbtnDeactivate");
                LinkButton lbtnActivate = (LinkButton)row.FindControl("lbtnActivate");

                if (lbtnDeactivate != null && lbtnActivate != null)
                {
                    DataRowView dataRowView = (DataRowView)row.DataItem;
                    if (dataRowView != null)
                    {
                        bool hkStatus = false;
                        bool hoStatus = false;

                        if (dataRowView.Row.Table.Columns.Contains("HKStatus") && dataRowView["HKStatus"] != DBNull.Value)
                        {
                            hkStatus = Convert.ToBoolean(dataRowView["HKStatus"]);
                        }

                        if (dataRowView.Row.Table.Columns.Contains("HOStatus") && dataRowView["HOStatus"] != DBNull.Value)
                        {
                            hoStatus = Convert.ToBoolean(dataRowView["HOStatus"]);
                        }

                        // For "All Users"
                        if (hkStatus || hoStatus)
                        {
                            lbtnDeactivate.Visible = true;
                            lbtnActivate.Visible = false;
                        }
                        else
                        {
                            lbtnDeactivate.Visible = false;
                            lbtnActivate.Visible = true;
                        }
                    }
                }
            }
        }

        protected void ddlAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccount.SelectedValue == "1")
            {
                BindHousekeeperData();
                gvs.Visible = false;
                gvsUsers.Visible = true;
            }
            else if (ddlAccount.SelectedValue == "2")
            {
                BindHouseOwnerData();
                gvs.Visible = false;
                gvsUsers.Visible = true;
            }
            else if (ddlAccount.SelectedValue == "-1")
            {
                BindUserData();
                gvs.Visible = true;
                gvsUsers.Visible = false;
            }
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewProfileJob = (LinkButton)sender;
            string user_id = lnkViewProfileJob.CommandArgument;

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT u.*, l.District, " +
                              "CASE " +
                              "WHEN hk.HK_User_ID IS NOT NULL THEN 'Housekeeper' " +
                              "WHEN ho.HO_User_ID IS NOT NULL THEN 'House Owner' " +
                              "ELSE 'Unknown' END AS UserType " +
                              "FROM tblUser u " +
                              "LEFT JOIN tblHousekeeper hk ON u.User_ID = hk.HK_User_ID " +
                              "LEFT JOIN tblHouseowner ho ON u.User_ID = ho.HO_User_ID " +
                              "JOIN tblLocation l ON u.User_City = l.Loc_ID " +
                              "WHERE u.User_ID = @uid";
            cmd.Parameters.AddWithValue("@uid", user_id);
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
                string userType = dr["UserType"].ToString(); // Get the user type

                lblLastName.Text = $"<strong>Last Name:</strong> {lastname}";
                lblFirstName.Text = $"<strong>First Name:</strong> {firstname}";
                lblEmail.Text = $"<strong>Email:</strong> {email}";
                lblLocation.Text = $"<strong>Location:</strong> {location}";
                lblPhone.Text = $"<strong>Phone:</strong> {phone}";
                lblUserType.Text = $"<strong>User Type:</strong> {userType}";
            }
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "viewUserModal();", true);
        }

        protected void lbtnDeactivate_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewProfileJob = (LinkButton)sender;
            string user_id = lnkViewProfileJob.CommandArgument;

            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tblHousekeeper SET HK_Status = 0 WHERE HK_User_ID = @user_id;" +
                              "UPDATE tblHouseowner SET HO_Status = 0 WHERE HO_User_ID = @user_id";
            cmd.Parameters.AddWithValue("@user_id", user_id);
            con.Open();

            IsUpdated = cmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                lblMessage.Text = "User Deactivated Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "viewMessageModal();", true);
                if (ddlAccount.SelectedValue == "1")
                {
                    BindHousekeeperData();
                    gvsUsers.Visible = true;
                }
                else if (ddlAccount.SelectedValue == "2")
                {
                    BindHouseOwnerData();
                    gvsUsers.Visible = true;
                }
                else if (ddlAccount.SelectedValue == "-1")
                {
                    BindUserData();
                }
            }
            else
            {
                lblMsg.Text = "Error While Deactivated User Account";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate");
                LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate");

                DataRowView rowView = e.Row.DataItem as DataRowView;
                if (rowView != null && lbtnDeactivate != null && lbtnActivate != null)
                {
                    bool hkStatus = rowView.Row.Table.Columns.Contains("HKStatus") &&
                                    rowView["HKStatus"] != DBNull.Value &&
                                    Convert.ToBoolean(rowView["HKStatus"]);

                    bool hoStatus = rowView.Row.Table.Columns.Contains("HOStatus") &&
                                    rowView["HOStatus"] != DBNull.Value &&
                                    Convert.ToBoolean(rowView["HOStatus"]);

                    if (ddlAccount.SelectedValue == "1") // For Housekeepers
                    {
                        lbtnDeactivate.Visible = hkStatus;
                        lbtnActivate.Visible = !hkStatus;
                    }
                    else if (ddlAccount.SelectedValue == "2") // For House Owners
                    {
                        lbtnDeactivate.Visible = hoStatus;
                        lbtnActivate.Visible = !hoStatus;
                    }
                    else if (ddlAccount.SelectedValue == "-1") // For "All Users"
                    {
                        // Show deactivate button if either HKStatus or HOStatus is true
                        if (hkStatus || hoStatus)
                        {
                            lbtnDeactivate.Visible = true;
                            lbtnActivate.Visible = false;
                        }
                        else
                        {
                            lbtnDeactivate.Visible = false;
                            lbtnActivate.Visible = true;
                        }
                    }
                }
            }
        }

        protected void gvsUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDeactivate = (LinkButton)e.Row.FindControl("lbtnDeactivate1");
                LinkButton lbtnActivate = (LinkButton)e.Row.FindControl("lbtnActivate1");

                if (lbtnDeactivate != null && lbtnActivate != null)
                {
                    DataRowView rowView = e.Row.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        bool hkStatus = rowView.Row.Table.Columns.Contains("HK_Status") &&
                                        rowView["HK_Status"] != DBNull.Value &&
                                        Convert.ToBoolean(rowView["HK_Status"]);

                        bool hoStatus = rowView.Row.Table.Columns.Contains("HO_Status") &&
                                        rowView["HO_Status"] != DBNull.Value &&
                                        Convert.ToBoolean(rowView["HO_Status"]);

                        string selectedValue = ddlAccount.SelectedValue;

                        if (selectedValue == "1")
                        {
                            lbtnDeactivate.Visible = hkStatus;
                            lbtnActivate.Visible = !hkStatus;
                        }
                        else if (selectedValue == "2")
                        {
                            lbtnDeactivate.Visible = hoStatus;
                            lbtnActivate.Visible = !hoStatus;
                        }
                        else
                        {
                            lbtnDeactivate.Visible = false;
                            lbtnActivate.Visible = false;
                        }
                    }
                }
            }
        }

        protected void lbtnActivate1_Click(object sender, EventArgs e)
        {
            LinkButton lnkViewProfileJob = (LinkButton)sender;
            string user_id = lnkViewProfileJob.CommandArgument;

            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE tblHousekeeper SET HK_Status = 1 WHERE HK_User_ID = @user_id;" +
                              "UPDATE tblHouseowner SET HO_Status = 1 WHERE HO_User_ID = @user_id";
            cmd.Parameters.AddWithValue("@user_id", user_id);
            con.Open();

            IsUpdated = cmd.ExecuteNonQuery() > 0;

            con.Close();
            if (IsUpdated)
            {
                lblMessage.Text = "User Activated Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "viewMessageModal();", true);
                if (ddlAccount.SelectedValue == "1")
                {
                    BindHousekeeperData();
                    gvsUsers.Visible = true;
                }
                else if (ddlAccount.SelectedValue == "2")
                {
                    BindHouseOwnerData();
                    gvsUsers.Visible = true;
                }
                else if (ddlAccount.SelectedValue == "-1")
                {
                    BindUserData();
                }
            }
            else
            {
                lblMsg.Text = "Error While Activating User Account";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ddlAccount_PreRender(object sender, EventArgs e)
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

        protected void gvsUsers_PreRender(object sender, EventArgs e)
        {
            if (gvsUsers.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvsUsers.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvsUsers.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
        }
    }
}