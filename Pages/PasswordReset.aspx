<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs" Inherits="FitnessTracker.Pages.PasswordReset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
</head>
<body>
    <h1 class="text-center" id="loginHeadingSpacer">Forgot Password?</h1>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row align-items-center text-center">
                <div class="col-5 text-end">
                    <asp:Label ID="lbl_email" runat="server" Text="email: " CssClass="basicLabels"></asp:Label>
                </div>
                <div class="col-3">
                    <asp:TextBox ID="txtbx_email" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="container" id="profileButtons">
            <div class="row">
                <div class="col-sm align-self-center text-center">
                    <asp:Button ID="btn_forgotSubmit" runat="server" Text="Send email" CssClass="profileButtons" />
                </div>
                </div>
            </div>
    </form>
</body>
</html>
