using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ExercisePerson
    {
        /*

        Module Name: ExercisePerson

        Descriptiopn: The model that encapsulates the ExercisePerson. Represents an exercise that has actually been scheduled.

        Fields:     int ExercisePersonID
                    int ExerciseTemplateID
                    int UserID
                    DateTime DateScheduled

        Programmer(s)'s Names:  Chadiwck Mayer

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The unique identifier of this ExercisePerson
        public int ExercisePersonID { get; set; }

        //The ExerciseTemplate assocated with this ExercisePerson. Determines the units, muscle group and exercise name.
        public int ExerciseTemplateID { get; set; }

        //The UserID associated with this ExercisePerson
        public int UserID { get; set; }

        //The date for which this ExercisePerson is scheduled
        public DateTime DateScheduled { get; set; }

        public ExercisePerson(int exercisetemplateid = -1, int userid = -1, DateTime datescheduled = new DateTime(), int exercisepersonid = -1)
        {
            //Constructor

            ExerciseTemplateID = exercisetemplateid;
            UserID = userid;
            DateScheduled = datescheduled;
            ExercisePersonID = exercisepersonid;

        }
    }
}