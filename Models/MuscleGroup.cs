using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class MuscleGroup
    {
        /*

        Module Name: MuscleGroup

        Descriptiopn:   The model that encapsulates the MuscleGroup.                        

        Fields:     int MuscleGroupID
                    string MuscleGroupName

        Programmer(s)'s Names:  Dustin Schlatter

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The unique identifier of the MuscleGroup
        public int MuscleGroupID { get; set; }

        //The name of the MuscleGroup
        public string MuscleGroupName { get; set; }

        public MuscleGroup(string musclegroupname = "", int musclegroupid = -1)
        {
            //Constructor

            MuscleGroupName = musclegroupname;
            MuscleGroupID = musclegroupid;

        }

    }
}