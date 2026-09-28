<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="LeaveApplications.Calendar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Calendar</title>
    <style>
        body { font-family: 'Segoe UI', Arial, sans-serif; background-color: #f3f4f6; padding: 40px; display: flex; justify-content: center; }
        .calendar-card { background: #fff; padding: 25px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); border-top: 4px solid #2563eb; }
        .btn-apply { background: #2563eb; color: #fff; border: none; padding: 10px 20px; font-weight: bold; border-radius: 4px; cursor: pointer; margin-top: 15px; width: 100%; }
        .btn-apply:hover { background: #1d4ed8; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="calendar-card">
            <h2>Select Leave Date</h2>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="220px" Width="300px">
                <SelectedDayStyle BackColor="#2563eb" Font-Bold="True" ForeColor="White" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            </asp:Calendar>
            <asp:Button ID="Button1" runat="server" Text="Apply for Leave" CssClass="btn-apply" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>