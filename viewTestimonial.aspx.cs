using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;

namespace MoCoolMaid
{
    public partial class viewTestimonial : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["MoCoolMaidCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTestimonial();
        }
        private void BindTestimonial()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblUser u, tblTestimonial t WHERE u.User_ID = t.User_ID";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtTestimonial = new DataTable();

            using (da)
            {
                da.Fill(dtTestimonial);
            }

            lvTestimonials.DataSource = dtTestimonial;
            lvTestimonials.DataBind();
        }

        protected void lvTestimonials_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindTestimonial();
        }
    }
}