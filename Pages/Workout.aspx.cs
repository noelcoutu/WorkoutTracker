using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;


namespace FitnessTracker.Pages
{
    /*

    Module Name: Workout Controller

    Descriptiopn: The code behind for the Workout view (Workout.aspx). Contains all the event handlers for the Workout view.

    Variables:  List<ExercisePerson> exercises
                List<ExerciseTemplate> allExerciseTemplates
                List<MuscleGroup> allMuscleGroups
                Session Keys:   Session["user"]
                                Session["Workout_Date"]
                   
    Programmer(s)'s Names:  Salman Almerekhi
                            Chadiwck Mayer

    Date Written: 15 Aug 2021

    Version Number 2.0

    */
    public partial class Workout : System.Web.UI.Page
    {
        //Holds all the exercises returned for the user on the selected date
        protected List<ExercisePerson> exercises;

        //Holds all exercise templates
        protected List<ExerciseTemplate> allExerciseTemplates;

        //Holds all muscle groups
        protected List<MuscleGroup> allMuscleGroups;

        protected void Page_Load(object sender, EventArgs e)
        {

            //Get ExerciseTemplates and MuscleGroups from database
            allExerciseTemplates = DB.GetAllExerciseTemplates();
            allMuscleGroups = DB.GetAllMuscleGroups();

            if (!Page.IsPostBack)
            {
                //Redirects to the Login page if a user has not loggeed in
                if (Session["User"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                if (allExerciseTemplates != null)
                {

                    //Build the drop down list that will be used for the addition of new exercises
                    ddl_exerciseList.DataSource = allExerciseTemplates;
                    ddl_exerciseList.DataTextField = "ExerciseName";
                    ddl_exerciseList.DataValueField = "ExerciseID";
                    ddl_exerciseList.DataBind();

                }

                DateTime date = (DateTime)Session["Workout_Date"];

                lbl__workout_date.Text = date.ToString("MMMM d, yyyy");
            }

            //Populate the workout with its workout cards          
            BuildWorkout();

        }

        protected void Btn_Okay_Click(object sender, EventArgs e)
        {
            //Get Session key values to add exercise
            var date = (DateTime)Session["Workout_Date"];
            var user = (User)Session["User"];

            //Set the selected ExerciseTemplate from the drop down list to ad exercise to database
            var templateId = Convert.ToInt32(ddl_exerciseList.SelectedValue);

            //Add exercise to the database
            DB.AddExercisePerson(new ExercisePerson { DateScheduled = date, ExerciseTemplateID = templateId, UserID = user.UserID });


            //Populate the workout with its workout cards
            BuildWorkout();

        }

        protected void BuildWorkout()
        {
            //Pulls exercises from the currently selected date from the Database.
            //Adds each WorkoutButton to the page

            //Query database and populate fields with results
            exercises = DB.GetExercisesByDateAndUser((User)Session["User"], (DateTime)Session["Workout_Date"]);

            //Check to ensure there are no workout cards in the placeholder.
            //This prevents duplicate cards being added to the placeholder.
            if (placeholder_workoutcards.Controls != null)
            {
                placeholder_workoutcards.Controls.Clear();
            }

            if (exercises != null)
            {

                //Cycle through the returned exercses, build workout buttons for each one, and add to the container
                foreach (ExercisePerson exercise in exercises)
                {

                    Button WorkoutButton = new Button();

                    BuildWorkoutButton(WorkoutButton, exercise);

                    placeholder_workoutcards.Controls.Add(WorkoutButton);

                }
            }
        }

        protected void BuildWorkoutButton(Button WorkoutButton, ExercisePerson Exercise)
        {
            
            //Find the ExrciseTemplate to get the exercise naame.
            //This will be placed within the WorkoutButton text.
            ExerciseTemplate et = allExerciseTemplates.Find(x => x.ExerciseID == Exercise.ExerciseTemplateID);

            //Setting the WorkoutButton ID to the ExercisePerson ID wll allow us to find the correct ExercisePerson
            //when then button fires a click event.
            WorkoutButton.ID = Exercise.ExercisePersonID.ToString();

            WorkoutButton.Text = et.ExerciseName;

            WorkoutButton.Click += new EventHandler(WorkoutButton_Click);

            WorkoutButton.CssClass = "workoutBoxStyle";
        }

        protected void WorkoutButton_Click(object sender, EventArgs e)
        {
            //The event handler that will be assigned to every dynamically-geenrated WorkoutButton.
            //Accomplishes the following:
            //  1. Pulls the ExercisePerson ID from the WorkoutButton ID
            //  2. Finds the ExercisePerson in the exercises list
            //  3. Assigns the ExercisePerson to the "ExercisePerson" session key.
            //  4. Redirects to the "Exercise.aspx" page

            Button btn = (Button)sender;

            Session["ExercisePerson"] = exercises.Find(x => x.ExercisePersonID == int.Parse(btn.ID));

            Response.Redirect("Exercise.aspx");
        }

        protected void sign_out_Click(object sender, EventArgs e)
        {
            //Event Handler for the Sign Out button
            //Clears all values from the Session object and rediects to Login page

            Session.Contents.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void btn_backToCalendar_Click(object sender, EventArgs e)
        {
            //Clear workout date and redirect back to the Home page (Calendar)
            Session["Workout_Date"] = null;
            Response.Redirect("Home.aspx");
        }
    }

}
