using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XST.Service.Interfaces;
using XST.Service.Utils.Enums;

namespace XST.Service.Services
{
    public class DateTimeService : IDateTime
    {
        public int CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date > endDate.Date)
                throw new ArgumentException($"Incorrect end date: {endDate}. endDate should be greater than startDate.");

            int workingDays = (endDate.Date - startDate.Date).Days;
            // exclude the stardate and enddate
            if (workingDays < 2) return 0;
            // 1 week = 7 days
            int totalWeeks = workingDays / 7;

            if (workingDays > totalWeeks * 7)
            {
                int firstDayOfWeek = startDate.Date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)startDate.Date.DayOfWeek;
                int lastDayOfWeek = endDate.Date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)endDate.Date.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek < 6)
                {
                    if (lastDayOfWeek > 6)// Saturday and Sunday are in the remaining time interval
                        workingDays -= 2;
                    else if (lastDayOfWeek > 5)// Saturday is in the remaining time interval
                        workingDays -= 1;
                }
                else if (firstDayOfWeek < 7 && lastDayOfWeek > 7)// Only Sunday is in the remaining time interval
                    workingDays -= 1;
            }

            workingDays -= (2 * totalWeeks) + 1;

            return workingDays;
        }

        public int CalculateWorkingDays(DateTime startDate, DateTime endDate, IEnumerable<DateTime> holidays)
        {
            int workingDays = CalculateWorkingDays(startDate, endDate);

            foreach (var daysOff in holidays)
            {
                DateTime holiday = daysOff.Date;
                // check if holiday between the dates
                // check if holiday is in weekdays
                if (startDate.Date < holiday && endDate > holiday 
                    && !(holiday.DayOfWeek == DayOfWeek.Sunday || holiday.DayOfWeek == DayOfWeek.Saturday))
                    workingDays--;
            }

            return workingDays;
        }

        public int CalculateWorkingDays(DateTime startDate, DateTime endDate, Dictionary<IEnumerable<DateTime>, HolidayType> holidays)
        {
            int workingDays = CalculateWorkingDays(startDate, endDate);

            foreach (var item in holidays)
            {
                switch (item.Value)
                {
                    case HolidayType.AlwaysOnSameDayEvenWeekend:
                    case HolidayType.CertainDayInMonth:
                        workingDays = CalculateWorkingDays(startDate, endDate, item.Key);
                        break;
                    case HolidayType.OnSameDayIfNotWeekend:
                        for (int iterator = 0; iterator < item.Key.Count(); iterator++)
                        {
                            DateTime holiday = item.Key.ElementAt(iterator).Date;
                            if (startDate.Date < holiday && endDate > holiday)
                                workingDays--;
                        }
                        break;
                    default:
                        break;
                }
            }


            return workingDays;
        }
    }
}
