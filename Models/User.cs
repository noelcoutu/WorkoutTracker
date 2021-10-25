using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Models
{
    public class User
    {
        /*

        Module Name: User

        Descriptiopn:   The model that encapsulates the User and all the user's attributes.                        

        Fields:     int UserID  
                    string UserName
                    string FirstName
                    string LastName
                    string Password
                    double Height
                    double Weight 
                    double WeightGoal
                    bool isMetric
                    DateTime Birthdate

        Programmer(s)'s Names:  Chadwick Mayer

        Date Written: 04 Aug 2021

        Version Number 2.0

        */
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double WeightGoal { get; set; }
        public bool isMetric { get; set; }
        public DateTime Birthdate { get; set; }

        public User(string firstName = "", string lastName = "", string username = "", string email = "", string password = "", double height = 0, double weight = 0, double weightGoal = 0,
            bool ismetric = false, DateTime birthdate = new DateTime(), int userID = -1)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Email = email;
            Password = password;
            Height = height;
            Weight = weight;
            WeightGoal = weightGoal;
            isMetric = ismetric;
            UserID = userID;
            Birthdate = birthdate;

        }
    }

}