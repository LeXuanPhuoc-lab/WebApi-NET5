using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Utils
{
    public class DateTimeHelper
    {
        public static int ToSpecificMonthValid(int totalMonth)
        {
            int currMonth = DateTime.Now.Month;
            int count = currMonth + totalMonth;
            if(count > 12)
            {
                return count = count - 12;
            }
            return -1;
        }

        public static List<DateTime> GetDates(int month, int year)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month)) // Days: 1,2,3...etc
                             .Select(day => new DateTime(year, month, day)) // Init DateTime foreach day
                             .ToList();
        }


        public static List<DateTime> GenerateRangeFromToDateTime(int totalMonth)
        {
            var dateTimes = new List<DateTime>();
            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;
            for (int i = 1; i <= totalMonth; ++i)
            {
                if (currMonth + i <= 12)
                {
                    if(dateTimes.Count > 0)
                    {
                        var tempList = DateTimeHelper.GetDates(currMonth + i, currYear);
                        foreach(DateTime dt in tempList)
                        {
                            dateTimes.Add(dt);
                        }
                    }
                    else
                    {
                        dateTimes = DateTimeHelper.GetDates(currMonth + i, currYear);
                    }
                }
            }

            int isOverValidMonth = DateTimeHelper.ToSpecificMonthValid(totalMonth);
            if (isOverValidMonth > 0)
            {
                var tempList = new List<DateTime>();
                for (int i = 1; i <= isOverValidMonth; ++i)
                {
                    tempList = DateTimeHelper.GetDates(i, currYear);
                    foreach (DateTime dt in tempList)
                    {
                        dateTimes.Add(dt);
                    }
                }
            }
             return dateTimes;
        }
    }
}
