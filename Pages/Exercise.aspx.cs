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

    Module Name: Exercise Controller

    Descriptiopn: The code behind for the Exercise view (Exercise.aspx). Contains all the event handlers for the Exercise view.

    Variables:  ExerciseTemplate ThisExerciseTemplate
                MuscleGroup ThisMuscleGroup
                List<Models.Unit> allUnits
                List<ExerciseUnit> exerciseUnits
                List<ExerciseEntry> exerciseEntries
                int[] setNumbers
                Dictionary<string, TextBox> PageTextBoxes
                Session Keys:   Session["user"]
                                Session["Workout_Date"]
                                Session["ExercisePerson"]
                   
    Programmer(s)'s Names:  Chadiwck Mayer

    Date Written: 20 Aug 2021

    Version Number 2.0

    */
    public partial class Exercise : System.Web.UI.Page
    {
        //Holds the ExerciseTemplate asociated with the ExercisePerson in the Session ExercisePerson Key
        protected ExerciseTemplate ThisExerciseTemplate;

        //Holds the MuscleGroup asociated with the ExercisePerson in the Session ExercisePerson Key
        protected MuscleGroup ThisMuscleGroup;

        //Holds all units
        protected List<Models.Unit> allUnits;

        //Holds the exercise units for this particular exercise
        protected List<ExerciseUnit> exerciseUnits;

        //Holds the exercise entreis for this particular exercise
        protected List<ExerciseEntry> exerciseEntries;

        //Holds all the distinct set numbers assoicated with the ExercisePerson
        protected int[] setNumbers;

        //Holds the Textboxes for the ExerciseEntries for the Add Set Box and the Edit Set Modal
        //This prevents having to search for the controls
        protected Dictionary<string, TextBox> PageTextBoxes = new Dictionary<string, TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("Login.aspx");
                }


            }
            //Poplate lists  with results from database queries
            ThisExerciseTemplate = DB.GetExerciseTemplateByExercisePerson((ExercisePerson)Session["ExercisePerson"]);
            ThisMuscleGroup = DB.GetMuscleGroupByExerciseTemplate(ThisExerciseTemplate);
            allUnits = DB.GetAllUnits();

            //Update the dynamic fields and build the add set box and the set list
            UpdateAddSetBoxAndSetList();

            //Add Exericse Box
            BuildExerciseBox(placeholder_Exercise_Box);
        }

       protected void BuildSetList(PlaceHolder SetListPlaceHolder)
        {
            //Builds the list of sets that the user has already completed for this exercise. 
            //This is displayed under the Add Exercise Box

            foreach(int setNumber in setNumbers)
            {
                //Build Container for Set Information
                //The ID for the container is the set number.
                //This allows for dynamic manipulation of exercise entries within the set.
                Panel SetContainer = new Panel();
                SetContainer.ID = "pnl_set_" + setNumber.ToString();
                SetContainer.CssClass = "row";

                //Get exercise entries with the current set number
                List<ExerciseEntry> entries = exerciseEntries.FindAll(x => x.SetNumber == setNumber);

                //Iterate through the etnries to build the panels and labels for each entry
                foreach(ExerciseEntry entry in entries)
                {

                    //Get the exercise unit of the exercise entry
                    ExerciseUnit exerciseunit = exerciseUnits.Find(x => x.ExerciseUnitID == entry.ExerciseUnitID);

                    //Get the unit of the exercise entry.
                    //The UnitName field will be used to create the text in the label
                    Models.Unit unit = allUnits.Find(x => x.UnitID == exerciseunit.UnitID);
                    
                    //Create and style the container for entry
                    //The ID for the panel will be the entry ID number
                    Panel EntryContainer = new Panel();
                    //EntryContainer.ID = entry.ExerciseEntryID.ToString();
                    EntryContainer.CssClass = "col-4 text-center";

                    //Create and style label for the exercise entry
                    Label EntryLabel = new Label();
                    //EntryLabel.ID = "lbl_exercise_entry_" + entry.ExerciseEntryID.ToString();
                    EntryLabel.CssClass = "basicLabels";
                    EntryLabel.Text = entry.ExerciseEntryValue.ToString() + " " + unit.UnitName;

                    EntryContainer.Controls.Add(EntryLabel);
                    SetContainer.Controls.Add(EntryContainer);

                }

                //Create edit button that will be used to edit the exercise entries in the set
                Button EditButton = new Button();
                EditButton.ID = "btn_edit_set_" + setNumber.ToString();
                EditButton.Text = "Edit";
                EditButton.CssClass = "exerciseButtons";
                EditButton.Click += Btn_Edit_Set_Click;

                //Create Edit Button container, style and add Edit Button
                Panel EditButtonContainer = new Panel();

                //THIS ATTRIBUTE IS USED TO EDIT THE EXERCISE ENTRIES ASSOCIATED WITH THE SET
                EditButton.Attributes.Add("name", setNumber.ToString());
                EditButtonContainer.ID = "pnl_edit_btn_set_" + setNumber.ToString();
                EditButtonContainer.CssClass = "col-2 text-center";
                EditButtonContainer.Controls.Add(EditButton);

                //Create delete button that will be used to delete the set
                Button DeleteButton = new Button();
                DeleteButton.ID = "btn_delete_set_" + setNumber.ToString();

                //THIS ATTRIBUTE IS USED TO DELETE THE EXERCISE ENTRIES ASSOCIATED WITH THE SET
                DeleteButton.Attributes.Add("name", setNumber.ToString());
                DeleteButton.Text = "Delete";
                DeleteButton.CssClass = "exerciseButtons";
                DeleteButton.Click += Btn_Delete_Set_Click;

                //Create Delete Button container, style and add Delete Button
                Panel DeleteButtonContainer = new Panel();
                DeleteButtonContainer.ID = "pnl_delete_btn_set_" + setNumber.ToString();
                DeleteButtonContainer.CssClass = "col-2 text-center";
                DeleteButtonContainer.Controls.Add(DeleteButton);

                //Add buttons to the Set Container
                SetContainer.Controls.Add(EditButtonContainer);
                SetContainer.Controls.Add(DeleteButtonContainer);

                SetListPlaceHolder.Controls.Add(SetContainer);

            }
        }
        protected void BuildExerciseBox(PlaceHolder ExerciseBoxPlaceHolder) 
        {
            //Method that builds the exercise block displayed at the top 

            //Build Exercise Name Label
            Label ExerciseNameLabel = new Label();
            ExerciseNameLabel.Text = ThisExerciseTemplate.ExerciseName;
            ExerciseNameLabel.ID = "lbl_Exercise_Name";
            ExerciseNameLabel.CssClass = "exerciseName";

            //Build container for Exercise Name Label
            Panel ExerciseNameContainer = new Panel();
            ExerciseNameContainer.ID = "pnl_Exercise_Name";
            ExerciseNameContainer.CssClass = "row align-items-center";

            //Add Exerice Name Label to Exercise Name Container
            ExerciseNameContainer.Controls.Add(ExerciseNameLabel);

            //Build Muscle Group Label
            Label MuscleGroupLabel = new Label();
            MuscleGroupLabel.Text = ThisMuscleGroup.MuscleGroupName;
            MuscleGroupLabel.ID = "lbl_Muscle_Group";
            MuscleGroupLabel.CssClass = "exerciseMuscleGroup";

            //Build container for Muscle Group Label
            Panel MuscleGroupContainer = new Panel();
            MuscleGroupContainer.ID = "pnl_Muscle_Group";
            MuscleGroupContainer.CssClass = "row align-items-center";

            //Add Muscle Group Label to Muscle Group container
            MuscleGroupContainer.Controls.Add(MuscleGroupLabel);

            //Style passed in panel and add containers to it
            Panel ExerciseBox = new Panel();
            ExerciseBox.CssClass = "exerciseBox text-center";
            ExerciseBox.Controls.Add(ExerciseNameContainer);
            ExerciseBox.Controls.Add(MuscleGroupContainer);

            ExerciseBoxPlaceHolder.Controls.Add(ExerciseBox);
        }

        protected void BuildAddSetBox(PlaceHolder AddSetBox)
        {

            foreach(ExerciseUnit exerciseunit in exerciseUnits)
            {
                //Get the unit for the display label
                Models.Unit unit = allUnits.Find(x => x.UnitID == exerciseunit.UnitID);

                //Create Label for the eercise entry
                Label ExerciseEntryLabel = new Label();
                ExerciseEntryLabel.ID = AddSetBox.ID + "_lbl_exercise_entry_" + exerciseunit.ExerciseUnitID.ToString();
                ExerciseEntryLabel.CssClass = "basicLabels";
                ExerciseEntryLabel.Text = unit.QualityMeasured + ":";

                Panel LabelContainer = new Panel();
                LabelContainer.CssClass = "col-6 text-center align-items-center";
                LabelContainer.Controls.Add(ExerciseEntryLabel);

                //Create text box for the exercise entry
                TextBox ExerciseEntryValue = new TextBox();
                ExerciseEntryValue.ID = AddSetBox.ID + "_txtbx_exercise_entry_" + exerciseunit.ExerciseUnitID.ToString();

                if(!PageTextBoxes.ContainsKey(AddSetBox.ID + "_txtbx_exercise_entry_" + exerciseunit.ExerciseUnitID.ToString()))
                {
                    PageTextBoxes.Add(AddSetBox.ID + "_txtbx_exercise_entry_" + exerciseunit.ExerciseUnitID.ToString(), ExerciseEntryValue);
                }

                Panel TextboxContainer = new Panel();
                TextboxContainer.CssClass = "col-6 text-center align-items-center";
                TextboxContainer.Controls.Add(ExerciseEntryValue);

                //Create hidden field that hols the Exercise Unit ID
                HiddenField ExerciseUnitID = new HiddenField();
                ExerciseUnitID.ID = AddSetBox.ID + "_hdn_exercise_unit_" + exerciseunit.ExerciseUnitID.ToString(); 
                ExerciseUnitID.Value = exerciseunit.ExerciseUnitID.ToString();

                //Create container for each exercise entry
                Panel EntryContainer = new Panel();
                EntryContainer.ID = AddSetBox.ID + "_pnl_exercise_entry_" + exerciseunit.ExerciseUnitID.ToString();
                EntryContainer.CssClass = "row align-items-center";

                //THIS ATTRIBUTE IS USED TO GET THE EXERCISEUNIT ID AND ADD THE EXERCISE ENTRY
                EntryContainer.Attributes.Add("name", exerciseunit.ExerciseUnitID.ToString());
                
                //Add all controls to the container
                EntryContainer.Controls.Add(LabelContainer);
                EntryContainer.Controls.Add(TextboxContainer);
                EntryContainer.Controls.Add(ExerciseUnitID);

                //Add the container to the set box
                AddSetBox.Controls.Add(EntryContainer);
            }
        }

        protected void Btn_Edit_Set_Click(object sender, EventArgs args)
        {

            //Build the set box within the edit set modal
            //UpdateAddSetBoxAndSetList();

            Button btn = (Button)sender;

            btn_save_changes.Attributes["name"] = btn.Attributes["name"];

            //Get all exercise etnries for this exercise that have the same set number as the "name" attribute of the button
            List<ExerciseEntry> entriesToEdit = exerciseEntries.FindAll(x => x.SetNumber == int.Parse(btn.Attributes["name"]));

            foreach(ExerciseEntry entry in entriesToEdit)
            {
                //Find the correct text box and set its Text attribute equal to the value of the exercise entry
                ((TextBox)(placeHolder_edit_set.FindControl(placeHolder_edit_set.ID + "_txtbx_exercise_entry_" + entry.ExerciseUnitID.ToString()))).Text = entry.ExerciseEntryValue.ToString();
            }

            //Displays the Edit Set Modal when the Edit button is clicked
            modal_edit_set.Show();
        }

        protected void Btn_Delete_Set_Click(object sender, EventArgs args)
        {
            //Event handler the all Delete Buttons.
            //Deletes all exercise entries asociated with the set and rebuilds the appropriate parts of the page

            Button btn = (Button)sender;

            //Get all exercise etnries for this exercise that have the same set number as the "name" attribute of the button
            List<ExerciseEntry> entriesToDelete = exerciseEntries.FindAll(x => x.SetNumber == int.Parse(btn.Attributes["name"]));

            foreach(ExerciseEntry entry in entriesToDelete)
            {
                DB.DeleteExerciseEntry(entry);
            }

            UpdateAddSetBoxAndSetList();

        }

        protected void btn_addExercise_Click(object sender, EventArgs e)
        {
            //Event Handler to add exercise entries to the database and rebuild the appropriate parts of the page.
            ExercisePerson ep = (ExercisePerson)Session["ExercisePerson"];

            //Increment the last set number
            int setNumber = setNumbers == null ? 1 : setNumbers.Last<int>() + 1;

            foreach(WebControl control in placeholder_Add_Set_Box.Controls)
            {

                //Get Exercise Unit ID
                int exerciseunitid = int.Parse(control.Attributes["name"]);

                //Find the correct text box and get its value
                int exerciseentryvalue = int.TryParse(((TextBox)(control.FindControl(placeholder_Add_Set_Box.ID + "_txtbx_exercise_entry_" + exerciseunitid.ToString()))).Text, out exerciseentryvalue) ? exerciseentryvalue : 0;

                //Add entry to database
                DB.AddExerciseEntry(new ExerciseEntry { ExercisePersonID = ep.ExercisePersonID, ExerciseUnitID = exerciseunitid, 
                    ExerciseEntryValue = exerciseentryvalue, SetNumber = setNumber});
            }

            UpdateAddSetBoxAndSetList();

        }

        protected void UpdateFields()
        {
            //Updates the fields that will change from the user manipulating the page

            exerciseEntries = DB.GetExerciseEntriesByExercisePerson((ExercisePerson)Session["ExercisePerson"]);
            exerciseUnits = DB.GetExerciseUnitByExerciseTemplate(ThisExerciseTemplate);
            setNumbers = DB.GetDistinctSetsByExercisePerson((ExercisePerson)Session["ExercisePerson"]);
        }

        protected void UpdateAddSetBoxAndSetList()
        {
            //
            UpdateFields();

            if (placeholder_set_list.Controls != null)
            {
                placeholder_set_list.Controls.Clear();
            }

            if (placeholder_Add_Set_Box.Controls != null)
            {
                placeholder_Add_Set_Box.Controls.Clear();
            }

            if (placeHolder_edit_set.Controls != null)
            {
                placeHolder_edit_set.Controls.Clear();
            }

            //Add the Add Set Box
            BuildAddSetBox(placeholder_Add_Set_Box);

            BuildAddSetBox(placeHolder_edit_set);

            //Add the Set List Box
            if(exerciseEntries != null)
            {
                BuildSetList(placeholder_set_list);
            }

        }

        protected void sign_out_Click(object sender, EventArgs e)
        {
            //Event Handler for the Sign Out button
            //Clears all values from the Session object and rediects to Login page

            Session.Contents.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void btn_save_changes_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            List<ExerciseEntry> entriesToUpdate = exerciseEntries.FindAll(x => x.SetNumber == int.Parse(btn.Attributes["name"]));

            foreach (ExerciseEntry entry in entriesToUpdate)
            {
                string value = PageTextBoxes[placeHolder_edit_set.ID + "_txtbx_exercise_entry_" + entry.ExerciseUnitID.ToString()].Text;
                entry.ExerciseEntryValue = double.Parse(PageTextBoxes[placeHolder_edit_set.ID + "_txtbx_exercise_entry_" + entry.ExerciseUnitID.ToString()].Text);

                DB.EditExerciseEntry(entry);
            }

            UpdateAddSetBoxAndSetList();

            Response.Redirect("Exercise.aspx");

        }

        protected void btn_backToWorkout_Click(object sender, EventArgs e)
        {
            //Clear ExercisePerson and redirect to the workout

            Session["ExercisePerson"] = null;
            Response.Redirect("Workout.aspx");
        }
    }
};