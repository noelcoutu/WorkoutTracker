using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extenders
{
    public static class DateTimeExtender
    {
        /*

        Module Name: DateTimeExtender

        Descriptiopn:   Contains two extension methods for the DateTime class.                       

        Fields:     None

        Programmer(s)'s Names:  Chadwick Mayer

        Date Written: 01 Aug 2021

        Version Number 2.0

        */

        public static DateTime StartOfMonth(this DateTime date)
        {
            //Returns the first day of the month of the currently instantiated DateTime object

            return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
        }
        public static DateTime EndOfMonth(this DateTime date)
        {
            //Returns the last day of the month of the currently instantiated DateTime object

            return date.StartOfMonth().AddMonths(1).AddSeconds(-1);
        }
    }
}
