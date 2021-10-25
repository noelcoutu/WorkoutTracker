using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ExerciseUnit
    {
        /*

        Module Name: ExerciseUnit

        Descriptiopn:   The model that encapsulates the ExerciseUnit. The ExerciseUnit ties an ExerciseTemplate with a specific unit of measurement.                        

        Fields:     int ExerciseTemplateID
                    int UnitID
                    int ExerciseUnitID

        Programmer(s)'s Names:  Dustin Schlatter

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The id of the ExerciseTemplate associated with this ExerciseUnit.
        public int ExerciseTemplateID { get; set; }

        //The id of the Unit assciated with this ExerciseUnit (lb, rep, mile, etc)
        public int UnitID { get; set; }

        //The unique identifier of this exercise unit
        public int ExerciseUnitID { get; set; }


        public ExerciseUnit(int exercisetemplateid = -1, int unitid = -1, int exerciseunitid = -1)
        {
            //Constructor

            ExerciseTemplateID = exercisetemplateid;
            UnitID = unitid;
            ExerciseUnitID = exerciseunitid;
        }
    }
}