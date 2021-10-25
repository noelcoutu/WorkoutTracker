<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exercise.aspx.cs" Inherits="FitnessTracker.Pages.Exercise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%-- 
    Module Name:    Exercise View

    Description:    The view for all the exercise entireds and sets asociated with the current exercise.
                    Displays the exercise name and muscle group in a box format.
                    Displays all current exercise enries in list format.
                    Includes controls for adding a set.
                    Includes controls for editing and deleting a set.

    Controls:       btn_backToWorkout
                    Button1
                    pnl_edit_set
                    btn_save_changes
                    btn_cancel_edit
                    modal_edit_set

    Placeholders:   placeholder_Exercise_Box
                    placeholder_Add_Set_Box
                    placeholder_set_list
                    placeHolder_edit_set

    Programmer(s)'s Names:  Noel Coutu
                            Chadwick Mayer

    Date Written: 13 Aug 2021

    Version:    2.0              
                    
    --%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
    <style>
        h2 {
            color: #E0E6E8;
        }
        .backToWorkoutButton{
            background-color: #E0E6E8;
            color: #2D3436;
            border-radius: 10px;
            height: 50px;
            width: 200px;
            border: solid 0px black;
            font-family: Nunito;
            font-weight: 800;
            margin: 0 auto;
            margin-bottom:50px;
           
        }
    </style>
    <script>

</script>
</head>
<body>
    <form id="form1" runat="server">
        <!--NAVBAR-->
        <nav class="navbar navbar-expand-sm navbar-dark" style="background-color: #636E72; color: yellow">
            <div class="container-fluid">
                <a class="navbar-brand" href="Home.aspx">Workout Tracker</a>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="UserProfile.aspx">User Profile</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="Home.aspx">Workouts</a>
                        </li>
                    </ul>

                </div>
                <asp:Button ID="sign_out" runat="server" Text="Sign Out" CssClass="headerButton" OnClick="sign_out_Click" />
            </div>
        </nav>


        <h1 id="exerciseHeadingSpacer" class="text-center">Exercise</h1>
        
        <div class="container">
            <%-- Back to workout button --%>
            <div class="row text-center">
                <asp:Button ID="btn_backToWorkout" runat="server" Text="back to workout" CssClass="backToWorkoutButton" OnClick="btn_backToWorkout_Click" />
            </div>

            <!--exercise box-->
            <div class="row align-content-center">
                <asp:PlaceHolder ID="placeholder_Exercise_Box" runat="server"></asp:PlaceHolder>
            </div>

            <!--this is the weight and reps boxes-->
            <asp:PlaceHolder ID="placeholder_Add_Set_Box" runat="server"></asp:PlaceHolder>

            <%-- Add exercise button --%>
            <div class="col-sm align-self-center text-center">
                <asp:Button ID="Button1" runat="server" Text="add exercise" class="exerciseButtons" OnClientClick="addExercise()" OnClick="btn_addExercise_Click" />
            </div>
        </div>

        <!--exercise stack-->
        <div class="container align-items-center">
            <div class="row">
                <div class="text-center align-self-center">
                    <h2>Completed:</h2>
                </div>
            </div>
            <asp:PlaceHolder ID="placeholder_set_list" runat="server"></asp:PlaceHolder>
        </div>

 
        <asp:ScriptManager ID="ScriptManager_form_edit_set" runat="server"></asp:ScriptManager>
        
        <%-- Panel for the edit set popup modal --%>
        <asp:Button ID="btn_edit1" runat="server" style="display:none"/>
        <asp:Panel ID="pnl_edit_set"  runat="server" >
            <asp:UpdatePanel ID="up_edit_set" runat="server">
                <ContentTemplate>
                    <div class="container popup">
                        <div class="row align-items-center text-center">
                            <h2>edit this set</h2>
                        </div>
                        <%-- Placeholder for the edit set labels and textboxes --%>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:PlaceHolder ID="placeHolder_edit_set" runat="server"></asp:PlaceHolder>
                        </asp:Panel>
                        <div class="row">
                            <%-- Save changes button --%>
                            <div class="col-sm align-self-center text-center">
                                <asp:Button ID="btn_save_changes" name="" runat="server" Text="Save" class="exerciseButtons" OnClick="btn_save_changes_Click"   />
                            </div>
                            <%-- Cancel button --%>
                            <div class="col-sm align-self-center text-center">
                                <asp:Button ID="btn_cancel_edit" runat="server" Text="Cancel" class="exerciseButtons" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <%-- Edit set popup modal --%>
        <cc1:ModalPopupExtender ID="modal_edit_set"  BehaviorID="modal_edit_set" runat="server" 
            TargetControlID="btn_edit1" CancelControlID="btn_cancel_edit" PopUpControlID="pnl_edit_set"
            BackgroundCssClass="modal-background"  />

    </form>
</body>
</html>
