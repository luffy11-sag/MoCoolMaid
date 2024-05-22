using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoCoolMaid.uc
{
    public partial class loginctrl2 : System.Web.UI.UserControl
    {
        public String Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Attributes["value"] = value; }
        }

        public bool Chk
        {
            get { return chkRememberMe.Checked; }
            set { chkRememberMe.Checked = value; }
        }

        public string EmailPlaceholder
        {
            get { return txtEmail.Attributes["placeholder"]; }
            set { txtEmail.Attributes["placeholder"] = value; }
        }

        public string ValidationGroup
        {
            set
            {
                RequiredFieldValidator1.ValidationGroup = value;
                RequiredFieldValidator2.ValidationGroup = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}