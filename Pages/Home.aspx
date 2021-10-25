<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FitnessTracker.Pages.Home" %>

<%--
    Module Name: Home View
    
    Descriptiopn: The view for all UI of the Home screen in the application. Displays the nav bar, a calendar with scheduled workout dates highlighted and buttons to go to the exercises in the selected date.

    Controls:   cal_workouts - selectable calendar that highlights dates where workouts are currently scheduled.
                btn_go_to_Workout - redirects the user to the date selected in the calendar (cal_workouts)  

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
    <link href="../Styles/StyleSheet1.css" rel="stylesheet" />
    <style>
        #calendarSpacer {
            margin-top: 5vh;
        }

        .calendar {
            border: #DFE6E9;
        }

        .dayHeader {
            text-align: center;
            padding-top: 25px;
            color: #DFE6E9;
            font-family: Nunito;
            font-weight: 900;
        }

        .dayStyle {
            font-family: Nunito;
            font-weight: 700;
            text-decoration: none;
        }

        .titleStyle {
            font-family: Roboto;
            font-size: 1.5em;
           
        }

        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--This is the navbar-->
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
            <asp:Button ID="sign_out" runat="server" Text="Sign Out" CssClass="headerButton" OnClick="sign_out_Click"/>
        </div>
    </nav>

        <div class="container" id="calendarSpacer">

            <%-- Calendar control that highlights dates where workouts are currently scheduled --%>
            <asp:Calendar ID="cal_workouts" runat="server" autopostback="true" OnDayRender="cal_workouts_DayRender" OnSelectionChanged="cal_workouts_SelectionChanged" NextMonthText="Next" PrevMonthText="Previous" OnVisibleMonthChanged="cal_workouts_VisibleMonthChanged" CssClass="calendar">
                <TitleStyle BackColor="#DFE6E9" Height="100px" CssClass="titleStyle"/>
                <DayStyle Height="150px" Width="200px" ForeColor="#636E72" Font-Underline="false" CssClass="dayStyle" />
                <DayHeaderStyle CssClass="dayHeader" />
            </asp:Calendar>

            <div class="text-center">
                <%-- Button that redirects the user to the date selected in the calendar (cal_workouts)  --%>
                <asp:Button ID="btn_go_to_Workout" runat="server" Text="Go" OnClick="btn_go_to_Workout_Click" CssClass="profileButtons" />
            </div>

        </div>
    </form>
</body>
</html>
