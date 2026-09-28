<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveForm.aspx.cs" Inherits="LeaveApplications.LeaveForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Leave Portal</title>
    <style>
        body { font-family: 'Segoe UI', Arial, sans-serif; background-color: #f3f4f6; margin: 0; padding: 20px; }
        .academic-wrapper { max-width: 650px; margin: 0 auto; }
        .university-header { display: flex; justify-content: space-between; align-items: center; background-color: #ffffff; padding: 12px 20px; border-radius: 8px 8px 0 0; border-bottom: 2px solid #2563eb; }
        .university-title { font-size: 16px; font-weight: 700; color: #1e293b; }
        .naac-badge { background-color: #b91c1c; color: #ffffff; padding: 3px 8px; border-radius: 4px; font-size: 11px; font-weight: 700; }
        .header-right { text-align: right; font-size: 11px; color: #475569; font-weight: 600; }
        .form-card { background-color: #ffffff; padding: 25px 30px; border-radius: 0 0 8px 8px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08); margin-bottom: 25px; }
        .form-card h3 { color: #1e3a8a; margin-top: 0; margin-bottom: 20px; font-size: 18px; border-bottom: 1px solid #e2e8f0; padding-bottom: 8px; }
        .form-group { margin-bottom: 14px; }
        .form-group label { display: block; font-weight: 600; font-size: 13px; color: #334155; margin-bottom: 5px; }
        .form-control { width: 100%; padding: 9px 12px; border: 1px solid #cbd5e1; border-radius: 5px; box-sizing: border-box; font-size: 13px; color: #1e293b; }
        .btn-submit { width: 100%; background-color: #1d4ed8; color: #ffffff; border: none; padding: 10px; font-size: 14px; font-weight: 600; border-radius: 5px; cursor: pointer; margin-top: 10px; }
        .btn-submit:hover { background-color: #1e40af; }
        .history-card { background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08); }
        .history-title { font-weight: 700; font-size: 15px; color: #1e293b; margin-bottom: 12px; }
        .gridview { width: 100%; border-collapse: collapse; font-size: 12px; }
        .gridview th { background-color: #f1f5f9; color: #334155; padding: 8px 10px; border: 1px solid #e2e8f0; text-align: left; }
        .gridview td { padding: 8px 10px; border: 1px solid #e2e8f0; color: #475569; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="academic-wrapper">
            <div class="university-header">
                <div>
                    <span class="university-title">Marwadi University</span>
                    <span class="naac-badge">NAAC A+</span>
                </div>
                <div class="header-right">
                    FACULTY OF ENGINEERING & TECHNOLOGY<br />
                    Department of Computer Engineering
                </div>
            </div>

            <div class="form-card">
                <h3>Leave Application Form</h3>

                <div class="form-group">
                    <label>Employee Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="e.g. ajit"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Leave Type</label>
                    <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">--Select Leave Type--</asp:ListItem>
                        <asp:ListItem Value="Sick Leave (SL)">Sick Leave (SL)</asp:ListItem>
                        <asp:ListItem Value="Casual Leave (CL)">Casual Leave (CL)</asp:ListItem>
                        <asp:ListItem Value="Privilege Leave (PL)">Privilege Leave (PL)</asp:ListItem>
                        <asp:ListItem Value="Leave Without Pay (LWP)">Leave Without Pay (LWP)</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <label>From Date</label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>To Date</label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Placeholder="e.g. N/A"></asp:TextBox>
                </div>

                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-submit" OnClick="btnSubmit_Click" />
            </div>

            <div class="history-card">
                <div class="history-title">Leave History Table</div>
                <asp:GridView ID="gvLeaveHistory" runat="server" CssClass="gridview" AutoGenerateColumns="true" GridLines="Both">
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>