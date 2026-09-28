using System;
using System.Web;

namespace LeaveApplications
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate == DateTime.MinValue)
            {
                Response.Write("<script>alert('Please select a date from the calendar first!');</script>");
                return;
            }

            if (Calendar1.SelectedDate.Date < DateTime.Today)
            {
                Response.Write("<script>alert('You cannot apply for leave on a past date!');</script>");
                return;
            }

            // Session me Date Save karein
            Session["SelectedLeaveDate"] = Calendar1.SelectedDate;
            Response.Redirect("LeaveForm.aspx");
        }
    }
}