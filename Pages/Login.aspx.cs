using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data.SQLite;

namespace FitnessTracker.Pages
{
    /*

        Module Name: Login Controller

        Descriptiopn: The code behind for the Login view (Login.aspx). Contains all the event handlers for the Login view.

        Variables:  Session Keys: Session["User"]

        Programmer(s)'s Names:  Salman Almerekhi
                                Dylan Gerrace

        Date Written: 08 Aug 2021

        Version Number 2.0

    */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Event handlewr for Page Load.
            //Empty because this is the landing page.
        }


        protected void btn_login_Click(object sender, EventArgs e)
        {
            //Check DB for user with user name that matche swhat was entered
            User user = DB.GetUserByUserName(txtbx_username.Text);

            if (user != null)
            {
                //The user name was found
                lbl_login_error.Visible = false;

                if (DB.VerifyHash(txtbx_password.Text, user.Password))
                {
                    //the password enetred matched
                    lbl_login_error.Visible = false;
                    Session["User"] = user;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    //The password did not match
                    user = null;
                    lbl_login_error.Text = "The password you entered is incorrect";
                    lbl_login_error.Visible = true;
                }
            }
            else
            {
                //The user name is not in the databse
                lbl_login_error.Text = "That username does not exist";
                lbl_login_error.Visible = true;
            }
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            //Send user to the registration page
            Response.Redirect("UserRegistration.aspx");
        }
    }
    
}