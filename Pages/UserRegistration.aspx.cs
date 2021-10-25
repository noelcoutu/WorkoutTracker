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

        Module Name: User Registration Controller

        Descriptiopn: The code behind for the User Registration view (Useregistration.aspx). Contains all the event handlers for the Registration view.

        Variables:  Session Keys: Session["user"]

        Programmer(s)'s Names:  Salman Almerekhi
                                Dylan Gerace

        Date Written: 08 Aug 2021

        Version Number 2.0

    */
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handler for Page Load.
            //Is empty since all content on the page is static
        }

        protected void val_unique_username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Checks database to make sure username is not already taken
            args.IsValid = (DB.GetUserByUserName(args.Value) == null);
        }

        protected void val_unique_email_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Checks database to make sure email is not already taken
            args.IsValid = (DB.GetUserByEmail(args.Value) == null);
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            //Performs additional data validation
            //Instantiates the user object
            //Adds the user to the database
            //Redirects the user to the Login View (Login.aspx).

            if (Page.IsValid)
            {

                //Prepare user inputs to instantiate user object
                string username = txtbx_username_signup.Text.ToLower();

                //Password is hashed before being written to database
                string password = DB.GetHash(txtbx_password_signup.Text); 

                //If the birthdate field is empty, the current date is assigned to it
                DateTime birthdate = DateTime.TryParse(txtbx_birthdate.Text, out birthdate) ? birthdate : DateTime.Now; 

                //Parse Height Textboxes
                double heightFeet = Double.TryParse(txtbx_height_feet.Text, out heightFeet) ? heightFeet : 0;
                double heightInches = Double.TryParse(txtbx_height_inches.Text, out heightInches) ? heightInches : 0;

                //Convert feet and inches to just inches
                double height = heightFeet * 12 + heightInches;

                //Parse weight Textboxes
                double weight = Double.TryParse(txtbx_weight.Text, out weight) ? weight : 0;
                double weight_goal = Double.TryParse(txtbx_weight_goal.Text, out weight_goal) ? weight_goal : 0;

                //Instantiate new user
                User user = new User(txtbx_first_name.Text, txtbx_last_name.Text, username,
                    txtbx_email.Text, password, height, weight, weight_goal, false, birthdate);

                DB.AddUser(user);

                Response.Redirect("Login.aspx");

            }
        }

    }
}