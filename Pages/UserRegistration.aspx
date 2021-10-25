<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="FitnessTracker.Pages.UserRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%-- 
    Module Name:    User Registration View

    Description:    The view for all UI of the User registration screen in the application. Displays the fields and labels for user registration, error/validation messages, and a button to sign up.

    Controls:       txtbx_username_signup
                    txtbx_password_signup
                    txtbx_password_confirm_signup
                    txtbx_email
                    txtbx_first_name
                    txtbx_last_name
                    txtbx_birthdate
                    txtbx_height_feet
                    txtbx_height_inches
                    txtbx_weight
                    txtbx_weight_goal
                    btn_signup

    Validators:     val_req_username_signup
                    val_unique_username
                    val_req_password_signup
                    val_regex_password
                    val_req_password_confirm_signup
                    val_compare_passwords
                    val_unique_email
                    val_req_emai
                    val_range_height_feet
                    val_range_height_inches
                    val_range_weight
                    val_range_weight_goal

    Programmer(s)'s Names:  Noel Coutu
                            Dustin Schlatter

    Date Written: 07 Aug 2021

    Version:    2.0              
                    
    --%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <style>
        .heightBoxes{
            width:50px;
        }
        .heightColor{
            color:#DFE6E9;
        }
    </style>
</head>
<body>


    <h1 class="text-center" id="headingSpacer">Create your account</h1>

    <form id="form_signup" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container moveItRight">

            <%-- Ensures password is at least eight charrcters long, contains one upperacse letter, one lowercase letter, one number and one special character --%>
            <div class="row align-items-center text-center">
              <asp:RegularExpressionValidator ID="val_regex_password" runat="server" ErrorMessage="Requires eight characters, one uppercase letter, one lowercase letter and one special character" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}" ControlToValidate="txtbx_password_signup" CssClass="validationError"></asp:RegularExpressionValidator>
            </div>
            <%-- The username label, control and validation --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1">
                    <asp:Label ID="lbl_username" runat="server" Text="username: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_username_signup" runat="server"></asp:TextBox>
                </div>
                <%-- Username required field validator --%>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="val_req_username_signup" runat="server" ErrorMessage="Username is required" ControlToValidate="txtbx_username_signup" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
                <%-- Enforces unique username --%>
                <div class="col-2">
                    <asp:CustomValidator ID="val_unique_username" runat="server" ErrorMessage="Username is already taken" ControlToValidate="txtbx_username_signup" OnServerValidate="val_unique_username_ServerValidate" CssClass="validationError"></asp:CustomValidator>
                </div>
            </div>

            <%-- The password label, control and validation --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1">
                    <asp:Label ID="lbl_password" runat="server" Text="password: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_password_signup" runat="server" TextMode="Password" ControlToValidate="txtbx_password_signup"></asp:TextBox>
                </div>
                <%-- Password required field validator --%>
                <div class="col-3">
                    <asp:RequiredFieldValidator ID="val_req_password_signup" runat="server" ErrorMessage="Password is required" ControlToValidate="txtbx_password_signup" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
                
            </div>

            <%-- The password confirmation label, control and validation --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1 ">
                    <asp:Label ID="lbl_passwordConfirm" runat="server" Text="verify password: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_password_confirm_signup" runat="server" TextMode="Password" CausesValidation="False"></asp:TextBox>
                </div>
                <%-- Password confirmation required field validator --%>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="val_req_password_confirm_signup" runat="server" ErrorMessage="Retype password" ControlToValidate="txtbx_password_confirm_signup" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
                <%-- Ensures the text in the password and password confirmation text boxes match --%>
                <div class="col-2">
                    <asp:CompareValidator ID="val_compare_passwords" runat="server" ErrorMessage="Passwords must match" ControlToCompare="txtbx_password_signup" ControlToValidate="txtbx_password_confirm_signup" CssClass="validationError"></asp:CompareValidator>
                </div>
            </div>

            <%-- The email label, control and validation --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1 ">
                    <asp:Label ID="lbl_email" runat="server" Text="email: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_email" runat="server" TextMode="Email"></asp:TextBox>
                </div>
                <%-- Email required field validator --%>
                <div class="col-1">
                    <asp:RequiredFieldValidator ID="val_req_email" runat="server" ErrorMessage="Enter an email" ControlToValidate="txtbx_email" CssClass="validationError"></asp:RequiredFieldValidator>
                </div>
                <%-- Enforces unique email --%>
                <div class="col-2">
                    <asp:CustomValidator ID="val_unique_email" runat="server" ErrorMessage="A user with that email already exists" OnServerValidate="val_unique_email_ServerValidate" ControlToValidate="txtbx_email" CssClass="validationError"></asp:CustomValidator>
                </div>
                
            </div>

            <%-- The first name label and control --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1">
                    <asp:Label ID="lbl_firstName" runat="server" Text="first name: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_first_name" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- The last name label and control --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1">
                    <asp:Label ID="lbl_lastName" runat="server" Text="last name: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_last_name" runat="server"></asp:TextBox>
                </div>
            </div>

            <%-- The birthdate label and control --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1 ">
                    <asp:Label ID="lbl_birthdate" runat="server" Text="birthdate: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_birthdate" runat="server"></asp:TextBox>  
                </div>
            </div>

            <%-- height labels, controls, and validtors --%>
            <asp:Panel ID="pnl_height_imperial" runat="server" Visible="true">
                <div class="row align-items-center">
                    <div class="col-3 offset-1">
                        <asp:Label ID="lbl_height_feet" runat="server" Text="height: " CssClass="basicLabels"></asp:Label>
                    </div>
                    <div class="col-4 text-end heightColor">
                        <asp:TextBox ID="txtbx_height_feet" runat="server" TextMode="Number" Text="0" CssClass="heightBoxes"></asp:TextBox>
                        ft
                    </div>
                    <div class="col-1 text-center heightColor">
                        <asp:TextBox ID="txtbx_height_inches" runat="server" TextMode="Number" Text="0" CssClass="heightBoxes"></asp:TextBox>
                        in
                    </div>
                    <%-- Feet must be between 0 and 12--%>
                    <div class="col-1">
                        <asp:RangeValidator ID="val_range_height_feet" runat="server" ErrorMessage="Enter a valid value" MaximumValue="12" MinimumValue="0" Type="Integer" ControlToValidate="txtbx_height_feet" CssClass="validationError"></asp:RangeValidator>
                    </div>               
                    <%-- Inches must be between 0 and 11 --%>
                    <div class="col-1">
                        <asp:RangeValidator ID="val_range_height_inches" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="11" Type="Integer" ControlToValidate="txtbx_height_inches" CssClass="validationError"></asp:RangeValidator>
                    </div>
                </div>
            </asp:Panel>

            <%-- weight labels, controls, and validators --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1">
                    <asp:Label ID="lbl_weight" runat="server" Text="weight: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_weight" runat="server" TextMode="Number" Text="0"></asp:TextBox>
                </div>
                <div class="col-1 heightColor">
                    lbs
                </div>
                <%-- Weight must be between 0 and 500 lbs --%>
                <div class="col-2">
                    <asp:RangeValidator ID="val_range_weight" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="500" Type="Integer" ControlToValidate="txtbx_weight" CssClass="validationError"></asp:RangeValidator>
                </div>
            </div>

            <%-- weight goal labels, controls, and validators --%>
            <div class="row align-items-center">
                <div class="col-3 offset-1 ">
                    <asp:Label ID="lbl_weightGoal" runat="server" Text="weight goal: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-5 text-end">
                    <asp:TextBox ID="txtbx_weight_goal" runat="server" TextMode="Number" Text="0"></asp:TextBox>
                </div>
                <%-- Weight must be between 0 and 500 lbs --%>
                <div class="col-1 heightColor">
                    lbs
                </div>
                <div class="col-2">
                    <asp:RangeValidator ID="val_range_weight_goal" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="500" Type="Integer" ControlToValidate="txtbx_weight_goal" CssClass="validationError"></asp:RangeValidator>
                </div>
            </div>

            
        </div>
        <%-- Sign up button --%>      
        <div class="col-sm align-self-center text-center">
            <asp:Button ID="btn_signup" runat="server" Text="Sign up" OnClick="btn_signup_Click" CssClass="profileButtons" />
        </div>
 
    </form>
</body>
<script>
    /*Script for birthdate datepicker calendar*/
    $(function ()  
    {

        var date = new Date();
        var currentMonth = date.getMonth();
        var currentDate = date.getDate();
        var currentYear = date.getFullYear();

        $('#txtbx_birthdate').datepicker(
        {  
            dateFormat: 'mm/dd/yy',  
            changeMonth: true,  
            changeYear: true,  
            yearRange: '1900:2100',
            maxDate: new Date(currentYear, currentMonth, currentDate)
        });  
})  
</script>
</html>
