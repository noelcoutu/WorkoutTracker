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

    Module Name: User Profile Controller

    Descriptiopn: The code behind for the User Profile view (UserProfile.aspx). Contains all the event handlers for the User Profile view.

    Variables:  Session Keys: Session["user"]


    Programmer(s)'s Names:  Salman Almerekhi
                            Dylan Gerace

    Date Written: 10 Aug 2021

    Version Number 2.0

*/
    public partial class UserProfile : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {
                //Redirects to the Login page if a user has not loggeed in
                if (Session["User"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                //Initialize page fields to the current user
                User user = (User)Session["user"];
                if (user != null)
                {
                    txtbx_name.Text = user.FirstName;
                    txtbx_lastname.Text = user.LastName;
                    txtbx_email.Text = user.Email;
                    txtbx_weight.Text = user.Weight.ToString();
                    txtbx_feet.Text = ((int)(user.Height / 12)).ToString();
                    txtbx_height.Text = ((int)(user.Height % 12)).ToString();
                    txtbx_weight_goal.Text = user.WeightGoal.ToString();
                    txtbx_age.Text = user.Birthdate.ToString("M/d/yyyy");
                }
            }

        }

        protected void btn_savechanges_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var _user = (User)Session["user"];
                var user = new User();
                user.FirstName = txtbx_name.Text;
                user.LastName = txtbx_lastname.Text;
                user.Email = txtbx_email.Text;

                //Parse birthdate
                DateTime birthdate = DateTime.TryParse(txtbx_age.Text, out birthdate) ? birthdate : DateTime.Now;
                user.Birthdate = birthdate;

                //Parse Height Textboxes
                double heightFeet = Double.TryParse(txtbx_feet.Text, out heightFeet) ? heightFeet : 0;
                double heightInches = Double.TryParse(txtbx_height.Text, out heightInches) ? heightInches : 0;

                //Convert feet and inches to just inches
                user.Height = heightFeet * 12 + heightInches;

                user.Weight = Convert.ToDouble(txtbx_weight.Text ?? "0");
                user.WeightGoal = Convert.ToDouble(txtbx_weight_goal.Text ?? "0");
                user.UserID = _user.UserID;
                
                //Write changes to databse
                DB.EditUser(user);

                //Assign Session the updated user
                Session["user"] = DB.GetUserById(user.UserID);
            } 
        }

        protected void btn_deleteacc_Click(object sender, EventArgs e)
        {
            modal_delete_confirm.Show();
        }

        protected void btn_close_pnl_delete_account_Click(object sender, EventArgs e)
        {
            modal_delete_confirm.Hide();
        }

        protected void btn_delete_account_Click(object sender, EventArgs e)
        {
            //Initialize user variable to pass to DB functions
            User user = (User)Session["user"];

            //Delete all ExerciseEntries and ExercisePersons associated with the user and then delete the user
            DB.DeleteExerciseEntryByUer(user);
            DB.DeleteExercisePersonByUser(user);
            DB.DeleteUser(user);

            //Set both the instance and session key to null
            user = null;
            Session["user"] = null;

            //Redirect to the Login page
            Response.Redirect("Login.aspx");
        }

        protected void sign_out_Click(object sender, EventArgs e)
        {
            //Event Handler for the Sign Out button
            //Clears all values from the Session object and rediects to Login page

            Session.Contents.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void val_unique_email_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Checks database to make sure email is not already taken
            args.IsValid = (args.Value == ((User)(Session["User"])).Email || (DB.GetUserByEmail(args.Value) == null));
        }
    }
}