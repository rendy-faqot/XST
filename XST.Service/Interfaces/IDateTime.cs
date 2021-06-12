using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XST.Service.Utils.Enums;

namespace XST.Service.Interfaces
{
    public interface IDateTime
    {
        int CalculateWorkingDays(DateTime startDate, DateTime endDate);
        int CalculateWorkingDays(DateTime startDate, DateTime endDate, IEnumerable<DateTime> holidays);
        int CalculateWorkingDays(DateTime startDate, DateTime endDate, Dictionary<IEnumerable<DateTime>, HolidayType> holidays);
    }
}
