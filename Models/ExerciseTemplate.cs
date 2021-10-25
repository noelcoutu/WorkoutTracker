using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ExerciseTemplate
    {
        /*

        Module Name: ExerciseTemplate

        Descriptiopn:   The model that encapsulates the ExerciseTemplate. An exercise template is the bones of the exercise and contains the data
                        unique to an exercise itself.

        Fields:     int ExerciseID
                    string ExerciseName
                    int MuscleGroupID

        Programmer(s)'s Names:  Dylan Gerace

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The unique identifier of this ExerciseTemplate
        public int ExerciseID { get; set; }

        //The name of the exercise
        public string ExerciseName { get; set; }

        //The id of the muscle group associated with this exercise (Chest, Legs, Cardio, etc)
        public int MuscleGroupID { get; set; }

        public ExerciseTemplate(string exercisename = "", int musclegroupid = -1, int exerciseid = -1)
        {
            //Constructor

            ExerciseID = exerciseid;
            ExerciseName = exercisename;
            MuscleGroupID = musclegroupid;

        }
    }
}