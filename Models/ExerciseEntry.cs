using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ExerciseEntry
    {

        /*
         * Weight measurements are stored as lbs;
         * Time measurements are stored as seconds;
         * Distance measurements are stored as miles;
         */

        /*

        Module Name: ExerciseEntry

        Descriptiopn: The modelt that encapsulates the ExerciseEntry.

        Fields:     int ExerciseEntryID
                    int ExercisePersonID
                    int ExerciseUnitID
                    double ExerciseEntryValue
                    int SetNumber

        Programmer(s)'s Names:  Chadiwck Mayer

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The id of the ExercisePerson associated with this ExerciseEntry
        public int ExercisePersonID { get; set; }

        //The id of the ExerciseUnit associated with this ExerciseEntry
        public int ExerciseUnitID { get; set; }

        //The value of this exercise entry
        public double ExerciseEntryValue { get; set; }

        //The unique identfier of this ExerciseEntry
        public int ExerciseEntryID { get; set; }

        //The SetNumber associated with this ExerciseEntry
        public int SetNumber { get; set; }

        public ExerciseEntry(int exercisepersonid = -1, int exerciseunitid = -1, double exerciseentryvalue = 0, int setnumber = -1, int exerciseentryid = -1)
        {
            //Constructor

            ExercisePersonID = exercisepersonid;
            ExerciseUnitID = exerciseunitid;
            ExerciseEntryValue = exerciseentryvalue;
            ExerciseEntryID = exerciseentryid;
            SetNumber = setnumber;

        }

    }
}