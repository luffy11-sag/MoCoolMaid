using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.Admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["adminun"])))
            {
                Response.Redirect("~/adminlogin.aspx?url=" +
               Server.UrlEncode(Request.Url.AbsoluteUri));
            }

        }
    }
}