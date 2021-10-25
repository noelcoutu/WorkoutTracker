<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workout.aspx.cs" Inherits="FitnessTracker.Pages.Workout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%-- 
    Module Name:    Workout View

    Description:    The view for all the exercises asociated with the current workout date.
                    Displays all exercises as clickable cards.
                    Includes a plus button to add an exercise and a modal popup panel to select the exercise to add.

    Controls:       sign_out
                    lbl__workout_date
                    placeholder_workoutcards
                    btn_addExercise
                    btn_backToCalendar
                    ScriptManager1
                    modal_Add_Exercise
                    pnl_addExercise
                    ddl_exerciseList
                    btn_Okay
                    btn_Cancel

    Programmer(s)'s Names:  Noel Coutu
                            Dylan Gerace

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
        .container {
            display: flex;
        }

        .workoutBoxStyle {
            background-color: #636E72;
            height: 200px;
            width: 200px;
            border-radius: 1em;
            padding: 10px;
            color: #DFE6E9;
            text-align: center;
            font-family: nunito;
            white-space: normal !important;
            word-wrap: break-word;
        }

            .workoutBoxStyle:hover {
                border: black;
            }

        #addExerciseSpacer {
            padding-bottom: 50px;
        }

        .dropdown {
            margin-bottom: 40px;
        }

        .addExerciseButton {
            background-color: transparent;
            height: 200px;
            width: 200px;
            border-radius: 1em;
            color: #636E72;
            border: 5px dotted #636E72;
            margin-left: 45%;
        }

            .addExerciseButton:hover {
                opacity: .8;
                background-color: #DFE6E9;
                height: 200px;
                width: 200px;
                border-radius: .5em;
                margin-left: 45%;
            }

        .backToCalendarButton {
            background-color: #E0E6E8;
            color: #2D3436;
            border-radius: 10px;
            height: 50px;
            width: 200px;
            border: solid 0px black;
            text-align: center;
            margin-top: 50px;
            font-family: Nunito;
            font-weight: 800;
            margin-left:45%;
        }
        #headingSpacerWorkout{
            padding-top:10vh;
        }
        .dateSpacer{
            color:  #E0E6E8;
            padding:5vh;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!--navBar-->
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

        <!--exercise boxes-->
        <h1 id="headingSpacerWorkout" class="text-center">Workout Builder</h1>

        <%-- Displays the date of the workout --%>
        <h2 class="dateSpacer text-center">
            <asp:Label ID="lbl__workout_date" runat="server" Text="Label"></asp:Label>
        </h2>


        <asp:Panel ID="pnl_workoutcard_container" runat="server" CssClass="container">

            <%-- The placeholder container for all dynamically generated workout boxes --%>
            <asp:PlaceHolder ID="placeholder_workoutcards" runat="server"></asp:PlaceHolder>

        </asp:Panel>

        <%-- Button for adding an exercise to the workout --%>
        <asp:Button ID="btn_addExercise" runat="server" Text="+" CssClass="addExerciseButton" />
        <br />

        <%-- Back to calendar Button --%>
        <asp:Button ID="btn_backToCalendar" runat="server" Text="back to calendar" CssClass="backToCalendarButton" OnClick="btn_backToCalendar_Click" />

        <!--modal popup stuff-->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <cc1:ModalPopupExtender ID="modal_Add_Exercise" runat="server"
            CancelControlID="btn_Cancel"
            TargetControlID="btn_addExercise" PopupControlID="pnl_addExercise"
            PopupDragHandleControlID="PopupHeader" Drag="true"
            BackgroundCssClass="modal-background"
            DropShadow="True" OnOkScript="Btn_Okay_Click" RepositionMode="RepositionOnWindowResize">
        </cc1:ModalPopupExtender>

        <%-- The panel associated with the modal popup --%>
        <asp:Panel ID="pnl_addExercise" Style="display: none" runat="server" CssClass="popup">
            <h2 class="text-center" id="addExerciseSpacer">add exercise</h2>
            <div class="row align-content-center">
                <asp:DropDownList ID="ddl_exerciseList" runat="server" CssClass="dropdown">
                </asp:DropDownList>
            </div>
            <div class="row">
                <div class="col-6 text-center">
                    <asp:Button ID="btn_Okay" runat="server" Text="Add" OnClick="Btn_Okay_Click" CssClass="profileButtons" />
                </div>
                <div class="col-6 text-center">
                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CssClass="profileButtons" />
                </div>
            </div>
        </asp:Panel>

    </form>
</body>
</html>
