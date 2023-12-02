using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace MoCoolMaid.HouseOwner
{
    public partial class jobPost : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindJobCategory();
                bindJobDistrict();

                ListItem defCategory = new ListItem("Select Job Category", "-1");
                ddlJobType.Items.Insert(0, defCategory);
                ListItem defDistrict = new ListItem("Select Job District", "-1");
                ddlJobDistrict.Items.Insert(0, defDistrict);
            }
        }

        private void bindJobCategory()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblJobCategory";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobCategories = new DataTable();
            using (da)
            {
                da.Fill(dtJobCategories);
            }
            ddlJobType.DataSource = dtJobCategories;
            ddlJobType.DataTextField = "JCategory_Name";
            ddlJobType.DataValueField = "JCategory_ID";
            ddlJobType.DataBind();
        }

        private void bindJobDistrict()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblLocation";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dtJobDistrict = new DataTable();
            using (da)
            {
                da.Fill(dtJobDistrict);
            }
            ddlJobDistrict.DataSource = dtJobDistrict;
            ddlJobDistrict.DataTextField = "District";
            ddlJobDistrict.DataValueField = "Loc_ID";
            ddlJobDistrict.DataBind();
        }

        private void ResetAll()
        {
            ddlJobType.SelectedIndex = 0;
            txtJobDescription.Text = "";
            txtJobDeadline.Text = "";
            txtJobSalary.Text = "";
            ddlJobDistrict.SelectedIndex = 0;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            Boolean IsAdded = false;
            string strDate = txtJobDeadline.Text;
            DateTime dt = Convert.ToDateTime(strDate);
            int ho_id = Convert.ToInt32(Session["hoid"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO tblJob (Job_Desc, Job_Deadline, Job_Status, Job_Salary, HO_ID, JCategory_ID, Loc_ID) VALUES (@desc, @deadline, @status, @salary, @hoid, @cid, @lid)";
            cmd.Parameters.AddWithValue("@desc", txtJobDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@deadline", dt);
            cmd.Parameters.AddWithValue("@status", 3);
            cmd.Parameters.AddWithValue("@salary", txtJobSalary.Text.Trim());
            cmd.Parameters.AddWithValue("@hoid", ho_id);
            cmd.Parameters.AddWithValue("@cid", ddlJobType.SelectedValue);
            cmd.Parameters.AddWithValue("@lid", ddlJobDistrict.SelectedValue);
            cmd.Connection = con;
            con.Open();

            IsAdded = cmd.ExecuteNonQuery() > 0;
            con.Close();

            if (IsAdded)
            {
                lblMsg.Text = "The Job Was Posted Successfully!";
                lblMsg.ForeColor = System.Drawing.Color.CadetBlue;
            }
            else
            {
                lblMsg.Text = "Error While Posting Job!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ResetAll();
        }       
    }
}