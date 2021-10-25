<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="FitnessTracker.Pages.UserProfile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%-- 
    Module Name:    User Registration View

    Description:    The view for all UI of the User profile screen in the application. Displays the fields and labels for user profile, error/validation messages, 
                    a button to save any changes and a button to delete the account.

    Controls:       txtbx_name
                    txtbx_lastname
                    txtbx_email
                    txtbx_age
                    txtbx_feet
                    txtbx_height
                    txtbx_weight
                    txtbx_weight_goal

    Validators:     val_req_email
                    val_unique_email
                    val_range_height_feet
                    val_range_height_inches
                    val_range_weight
                    val_range_weight_goal

    Programmer(s)'s Names:  Noel Coutu
                            Dustin Schlatter

    Date Written: 07 Aug 2021

    Version:      2.0              
                    
    --%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <style>
        .heightBoxes{
            width:40px;
        }
        .heightText{
            color:#DFE6E9;
        }
    </style>
</head>

<body>
    <form id="form_user_profile" runat="server">

    <!--This is the navbar-->
    <nav class="navbar navbar-expand-sm navbar-dark" style="background-color: #636E72; color: yellow">
        <div class="container-fluid">
            <a class="navbar-brand" href="Home.aspx">Workout Tracker</a>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link  active" aria-current="page" href="UserProfile.aspx">User Profile</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="Home.aspx">Workouts</a>
                    </li>
                </ul>

            </div>
            <asp:Button ID="sign_out" runat="server" Text="Sign Out" CssClass="headerButton" OnClick="sign_out_Click"/>
        </div>
    </nav>

    <!--This is the photo, the labels, and the forms-->
    <h1 class="text-center" id="headingSpacer">Profile</h1>
        <asp:ScriptManager ID="ScriptManager_form_user_profile" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="row align-items-center">
                <div class="col-4">
                    <img class:"img-fluid" src="../Images/profileicon.svg" id="profileicon"/>
                </div>
                <div class="col-sm-8">
                    <%-- First name label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_firstname" runat="server" Text="first name:  <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-6 text-start">
                            <asp:TextBox ID="txtbx_name" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <%-- Last name label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_lastname" runat="server" Text="last name:    <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-6 text-start">
                            <asp:TextBox ID="txtbx_lastname" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <%-- Email label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_email" runat="server" Text="email:  <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-3 text-start">
                            <asp:TextBox ID="txtbx_email" runat="server"></asp:TextBox>
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

                    <%-- Birthdate label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_age" runat="server" Text="date of birth:   <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-6 text-start">
                            <asp:TextBox ID="txtbx_age" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_height" runat="server" Text="height:  " CssClass="basicLabels"></asp:Label>
                        </div>
                        <%-- Height (feet) label and textbox --%>
                        <div class="col-2 text-center heightText">
                            <asp:TextBox ID="txtbx_feet" CssClass="heightBoxes" runat="server" TextMode="Number"></asp:TextBox>
                            ft
                        </div>
                        <%-- Height (inches) label and textbox --%>
                        <div class="col-2 text-start heightText">
                            <asp:TextBox ID="txtbx_height" CssClass="heightBoxes" runat="server" TextMode="Number"></asp:TextBox>
                            in
                        </div>
                        <%-- Feet must be between 0 and 12--%>
                        <div class="col-1">
                            <asp:RangeValidator ID="val_range_height_feet" runat="server" ErrorMessage="Enter a valid value" MaximumValue="12" MinimumValue="0" Type="Integer" ControlToValidate="txtbx_feet" CssClass="validationError"></asp:RangeValidator>
                        </div>               
                        <%-- Inches must be between 0 and 11 --%>
                        <div class="col-1">
                            <asp:RangeValidator ID="val_range_height_inches" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="11" Type="Integer" ControlToValidate="txtbx_height" CssClass="validationError"></asp:RangeValidator>
                        </div>
                    </div>
                    <%-- Weight label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_weight" runat="server" Text="weight:   <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-3 text-start">
                            <asp:TextBox ID="txtbx_weight" runat="server"></asp:TextBox>
                        </div>
                        <%-- Weight must be between 0 and 500 lbs --%>
                        <div class="col-2">
                            <asp:RangeValidator ID="val_range_weight" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="500" Type="Integer" ControlToValidate="txtbx_weight" CssClass="validationError"></asp:RangeValidator>
                        </div>                    
                    </div>
                    <%-- Weight Goal label and textbox --%>
                    <div class="row align-items-center">
                        <div class="col-6">
                            <asp:Label ID="lbl_weight_goal" runat="server" Text="weight goal:   <br>" CssClass="basicLabels"></asp:Label>
                        </div>
                        <div class="col-3 text-start">
                            <asp:TextBox ID="txtbx_weight_goal" runat="server"></asp:TextBox>
                        </div>
                        <%-- Weight Goal must be between 0 and 500 lbs --%>
                        <div class="col-2">
                            <asp:RangeValidator ID="val_range_weight_goal" runat="server" ErrorMessage="Enter a valid value" MinimumValue="0" MaximumValue="500" Type="Integer" ControlToValidate="txtbx_weight_goal" CssClass="validationError"></asp:RangeValidator>
                        </div>                    
                    </div>
                </div>
            </div>
        </div>

        <!--This is the container for the bottom buttons-->
        <div class="container" id="profileButtons">
            <div class="row">
                <div class="col-sm align-self-center text-center">
                    <asp:Button ID="btn_savechanges" runat="server" Text="save changes" class="profileButtons" OnClick="btn_savechanges_Click" />
                </div>
                
                <div class="col-sm align-self-center text-center">
                    <asp:Button ID="btn_deleteacc" runat="server" Text="delete account" class="profileButtons" OnClick="btn_deleteacc_Click" />
                </div>
            </div>
        </div>

        <%--This is the panel to confirm deletion of account--%>
        <asp:Button ID="btn_delete" runat="server" style="display:none"/>
        <asp:Panel ID="pnl_delete_account"  runat="server" >
            <asp:UpdatePanel ID="up_confrim_delete" runat="server">
                <ContentTemplate>
                    <div class="container popup">
                        <div class="row align-items-center text-center">
                            <h2>Are you sure you want to delete your account? This cannot be undone.</h2>
                        </div>
                        <div class="row">
                            <div class="col-sm align-self-center text-center">
                                <asp:Button ID="btn_delete_account" runat="server" Text="Yes" class="profileButtons" OnClick="btn_delete_account_Click"  />
                            </div>
                            <div class="col-sm align-self-center text-center">
                                <asp:Button ID="btn_close_pnl_delete_account" runat="server" Text="No" class="profileButtons" OnClick="btn_close_pnl_delete_account_Click"  />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <%--This is the modal popup to confirm deletion of account--%>
        <cc1:ModalPopupExtender ID="modal_delete_confirm"  BehaviorID="modal_delete_confirm" CancelControlID="btn_close_pnl_delete_account" runat="server" 
            TargetControlID="btn_delete" PopUpControlID="pnl_delete_account"
            BackgroundCssClass="modal-background"  />
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

        $('#txtbx_age').datepicker(
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
