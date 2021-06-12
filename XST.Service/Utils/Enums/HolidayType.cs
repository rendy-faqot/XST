using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XST.Service.Utils.Enums
{
    public enum HolidayType
    {
        AlwaysOnSameDayEvenWeekend = 1,
        OnSameDayIfNotWeekend,
        CertainDayInMonth
    }
}
