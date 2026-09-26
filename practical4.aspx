<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="practical4.aspx.cs" Inherits="Registration_Portal.practical4" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Event Registration Portal</title>
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            margin: 0;
            padding: 30px;
            display: flex;
            justify-content: center;
        }

        .form-card {
            background-color: #ffffff;
            padding: 30px 40px;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 650px;
        }

        h2 {
            color: #333333;
            text-align: center;
            margin-bottom: 25px;
            border-bottom: 2px solid #007bff;
            padding-bottom: 10px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        td {
            padding: 8px 5px;
            vertical-align: middle;
        }

        .label-col {
            font-weight: 600;
            color: #444444;
            width: 150px;
        }

        input[type="text"], 
        input[type="password"], 
        input[type="date"], 
        select {
            width: 100%;
            padding: 8px 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            font-size: 14px;
        }

        input[type="text"]:focus, 
        input[type="password"]:focus, 
        select:focus {
            border-color: #007bff;
            outline: none;
        }

        .btn-submit {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 25px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
            margin-top: 15px;
            transition: background-color 0.2s ease;
        }

        .btn-submit:hover {
            background-color: #0056b3;
        }

        .validator-msg {
            font-size: 12px;
            margin-left: 5px;
        }

        .success-lbl {
            display: block;
            text-align: center;
            margin-top: 15px;
            font-size: 15px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-card">
            <h2>Online Event Registration Form</h2>
            <table>
                <!-- Name -->
                <tr>
                    <td class="label-col">Name:</td>
                    <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- Username -->
                <tr>
                    <td class="label-col">User Name:</td>
                    <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- Password -->
                <tr>
                    <td class="label-col">Password:</td>
                    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- Confirm Password -->
                <tr>
                    <td class="label-col">Confirm Password:</td>
                    <td><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ErrorMessage="Passwords do not match" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:CompareValidator>
                    </td>
                </tr>

                <!-- City -->
                <tr>
                    <td class="label-col">City:</td>
                    <td>
                        <asp:DropDownList ID="ddlCity" runat="server">
                            <asp:ListItem Value="">-- Select City --</asp:ListItem>
                            <asp:ListItem>Hajipur</asp:ListItem>
                            <asp:ListItem>Ahmedabad</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" InitialValue="" ErrorMessage="Select city" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- Email -->
                <tr>
                    <td class="label-col">Email:</td>
                    <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <!-- Mobile -->
                <tr>
                    <td class="label-col">Mobile:</td>
                    <td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile is required" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ControlToValidate="txtMobile" ValidationExpression="^[0-9]{10}$" ErrorMessage="Enter 10 digits" ForeColor="Red" Display="Dynamic" CssClass="validator-msg"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <!-- Gender -->
                <tr>
                    <td class="label-col">Gender:</td>
                    <td>
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td><asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rblGender" ErrorMessage="Select gender" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- DOB -->
                <tr>
                    <td class="label-col">DOB:</td>
                    <td><asp:TextBox ID="txtDOB" runat="server" TextMode="Date"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="rfvDOB" runat="server" ControlToValidate="txtDOB" ErrorMessage="DOB is required" ForeColor="Red" CssClass="validator-msg"></asp:RequiredFieldValidator></td>
                </tr>

                <!-- Course -->
                <tr>
                    <td class="label-col">Course:</td>
                    <td>
                        <asp:CheckBoxList ID="cblCourse" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>AI</asp:ListItem>
                            <asp:ListItem>CSE</asp:ListItem>
                            <asp:ListItem>DA</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td></td>
                </tr>

                <!-- Profile -->
                <tr>
                    <td class="label-col">Profile:</td>
                    <td><asp:FileUpload ID="fuProfile" runat="server" /></td>
                    <td></td>
                </tr>

                <!-- Submit Button -->
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn-submit" />
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" CssClass="success-lbl"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>