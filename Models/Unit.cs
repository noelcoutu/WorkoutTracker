using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

    public class Unit
    {
        /*

        Module Name: Unit

        Descriptiopn:   The model that encapsulates the Unit (lb, reps, miles, minutes, etc.).                        

        Fields:     string UnitName 
                    string QualityMeasured
                    string UnitsSystem
                    int UnitID

        Programmer(s)'s Names:  Dustin Schlatter

        Date Written: 04 Aug 2021

        Version Number 2.0

        */

        //The name of the unit. This is what is displayed on most pages.
        public string UnitName { get; set; }

        //What the unit measures (weight, distance, time, etc.)
        public string QualityMeasured { get; set; }

        //The system it belongs to (Imperial, metric, gregorian, Arabic, Persian)
        public string UnitsSystem { get; set; }

        //The unique identifier of the unit
        public int UnitID { get; set; }

        public Unit(string unitname = "", string unitssystem="metric", int unitid = -1)
        {
            //Constructor

            UnitName = unitname;
            UnitsSystem = unitssystem;
            UnitID = unitid;

        }
    }

}