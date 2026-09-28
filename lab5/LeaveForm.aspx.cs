using System;
using System.Data;
using System.Web;

namespace LeaveApplications
{
    public partial class LeaveForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Read Cookie
                if (Request.Cookies["EmpNameCookie"] != null)
                {
                    txtName.Text = Request.Cookies["EmpNameCookie"].Value;
                }

                // 2. Read Session Date & Format correctly for HTML5 Date picker (yyyy-MM-dd)
                if (Session["SelectedLeaveDate"] != null)
                {
                    DateTime selectedDate = Convert.ToDateTime(Session["SelectedLeaveDate"]);
                    txtFromDate.Text = selectedDate.ToString("yyyy-MM-dd");
                }

                BindTable();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                Response.Write("<script>alert('Please enter Employee Name!');</script>");
                return;
            }

            if (ddlLeaveType.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please select a Leave Type!');</script>");
                return;
            }

            if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
            {
                Response.Write("<script>alert('Please select From Date and To Date!');</script>");
                return;
            }

            DateTime fromDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime toDate = Convert.ToDateTime(txtToDate.Text);

            if (fromDate.Date < DateTime.Today)
            {
                Response.Write("<script>alert('You cannot apply for leave on a past date!');</script>");
                return;
            }

            if (toDate < fromDate)
            {
                Response.Write("<script>alert('To Date cannot be earlier than From Date!');</script>");
                return;
            }

            // Save Cookie
            HttpCookie empCookie = new HttpCookie("EmpNameCookie", txtName.Text.Trim());
            empCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(empCookie);

            // Save to Session DataTable
            DataTable dt;
            if (Session["LeaveHistoryTable"] != null)
            {
                dt = (DataTable)Session["LeaveHistoryTable"];
            }
            else
            {
                dt = CreateLeaveTableSchema();
            }

            dt.Rows.Add(
                txtName.Text.Trim(),
                ddlLeaveType.SelectedValue,
                fromDate.ToString("dd-MM-yyyy"),
                toDate.ToString("dd-MM-yyyy"),
                string.IsNullOrWhiteSpace(txtRemarks.Text) ? "N/A" : txtRemarks.Text.Trim(),
                "Pending"
            );

            Session["LeaveHistoryTable"] = dt;
            BindTable();

            Response.Write("<script>alert('Leave Application Submitted Successfully!');</script>");
        }

        private void BindTable()
        {
            if (Session["LeaveHistoryTable"] != null)
            {
                gvLeaveHistory.DataSource = (DataTable)Session["LeaveHistoryTable"];
                gvLeaveHistory.DataBind();
            }
        }

        private DataTable CreateLeaveTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee");
            dt.Columns.Add("Leave Type");
            dt.Columns.Add("From Date");
            dt.Columns.Add("To Date");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Status");
            return dt;
        }
    }
}