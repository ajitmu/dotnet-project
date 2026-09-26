using System;
using System.Web.UI;

namespace Registration_Portal
{
    public partial class practical4 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblMessage.Text = "Registration Successful!";
            }
        }
    }
}