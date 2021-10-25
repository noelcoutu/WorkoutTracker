using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Models;

namespace FitnessTracker.Pages
{
    /*
      
    Module Name: Home Controller

    Descriptiopn: The code behind for the Homew view (Home.aspx). Contains all the event handlers for the Home view.

    Variables:  List<DateTime> workoutDate - Holds the dates where workouts are scheduled. Must be updated every time the clendar month changes.
                Session - Application-wide variable used to pass data between pages.
                Session Keys:   Session["User"]
                                Session["Workout_Date"]

    Programmer(s)'s Names:  Salman Almerekhi
                            Chadiwck Mayer

    Date Written: 08 Aug 2021

    Version Number 2.0
                            
    */

    public partial class Home : System.Web.UI.Page
    {

        //Holds the dates where workouts are scheduled. Must be updated every time the clendar month changes.
        protected List<DateTime> workoutDates;

        protected void Page_Load(object sender, EventArgs e)
        {

            if(Page.IsPostBack == false)
            {

                //Redirects to the Login page if a user has not loggeed in
                if(Session["User"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                //Initial query for the current month's workouts
                 workoutDates = DB.GetWorkoutDatesForMonth((User)Session["User"], DateTime.Now);
            }
        }

        protected void cal_workouts_DayRender(object sender, DayRenderEventArgs e)
        {          
            //Iterates through each day of the displayed month, checks to see if any of the dates in workoutDates matche the current date, and changes the cell color if true.

            if(workoutDates != null)
            {
                foreach (DateTime date in workoutDates)
                {
                    if (DateTime.Compare(date, e.Day.Date) == 0)
                    {
                        e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#DFE6E9");
                    }
                }
            }

        }

        protected void btn_go_to_Workout_Click(object sender, EventArgs e)
        {            
            Response.Redirect("Workout.aspx");
        }

        protected void cal_workouts_SelectionChanged(object sender, EventArgs e)
        {
            
            //DateTime object assigned to Session for database query in Workout.aspx
            Session["Workout_Date"] = cal_workouts.SelectedDate;

        }

        protected void cal_workouts_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            
            //Query for the workouts in the visible month
            workoutDates = DB.GetWorkoutDatesForMonth((User)Session["User"], e.NewDate);

        }
        protected void sign_out_Click(object sender, EventArgs e)
        {
            //Event Handler for the Sign Out button
            //Clears all values from the Session object and rediects to Login page

            Session.Contents.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}