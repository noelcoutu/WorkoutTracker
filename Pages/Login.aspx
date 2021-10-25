<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FitnessTracker.Pages.WebForm1" %>

<%--
    Module Name: Login View
    
    Descriptiopn:   The view for all UI of the Login screen in the application. Displays the nav bar, a fields to enter the username and password and buttons to login, sign up for 
                    an account.

    Controls:   txtbx_username: textbox for user to enter their user name.
                txtbx_password: textbox for user to enter their password.
    
    Validators: val_req_username: ensures a user enters a user name.
                val_req_password: ensures a user enters a password.

    Dynamic Labels: lbl_login_error: Displays the context-specific error message

    Programmer(s)'s Names:  Noel Coutu
                            Dustin Schlatter

    Date Written: 07 Aug 2021

    Version:    2.0
    --%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workout Tracker</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
</head>
<body>

    <!--This is the header, labels, and textboxes-->
    <h1 class="text-center" id="loginHeadingSpacer">Welcome to the Fitness Tracker</h1>
    <form id="form_login" runat="server">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-6 text-end">
                    <asp:Label ID="lbl_username" runat="server" Text="Username: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-6 text-start">
                    <asp:TextBox ID="txtbx_username" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row align-items-center">
                <div class="col-6 text-end">
                    <asp:Label ID="lbl_password" runat="server" Text="Password: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-6 text-start">
                    <asp:TextBox ID="txtbx_password" runat="server" TextMode="Password"></asp:TextBox>
                </div>
            </div>

            <!--These are the validation fields-->
            <div class="row">
                <div class="col text-center">
                    <asp:RequiredFieldValidator ID="val_req_username" runat="server" ErrorMessage="Username is required" ControlToValidate="txtbx_username" ValidationGroup="ValGroup1" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col text-center">
                    <asp:RequiredFieldValidator ID="val_req_password" runat="server" ErrorMessage="Password is required" ControlToValidate="txtbx_password" SetFocusOnError="True" ValidationGroup="ValGroup1" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col text-center">
                    <asp:Label ID="lbl_login_error" runat="server" Text="Label" Visible="False" CssClass="validationError"></asp:Label>
                </div>
            </div>
        </div>



        <!--This is the container for the bottom buttons-->
        <div class="container" id="profileButtons">
            <div class="row">
                <div class="col-sm align-self-center text-center">
                    <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" ValidationGroup="ValGroup1" CssClass="profileButtons" />
                </div>
                <<div class="col-sm align-self-center text-center">
                    <asp:Button ID="btn_signup" runat="server" Text="Sign up" OnClick="btn_signup_Click" CssClass="profileButtons" />
                </div>
                
            </div>
        </div>
    </form>
    
</body>
</html>
