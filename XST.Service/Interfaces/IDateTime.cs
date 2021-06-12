using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XST.Service.Interfaces
{
    public interface IDateTime
    {
        int CalculateWorkingDays(DateTime startDate, DateTime endDate);
        int CalculateWorkingDays(DateTime startDate, DateTime endDate, IEnumerable<DateTime> holidays);
    }
}
