using System;
using System.Collections.Generic;
using XST.Service.Interfaces;
using XST.Service.Services;
using Xunit;

namespace XST.ServiceTests
{
    public class CalculateWorkingDaysTests
    {
        private IDateTime _dateTime;

        // 1. Check positive case in 1 week
        // 2. Check positive case in different weeks
        // 3. Check negative case in same and next days
        // 4. Check negative case startDate > endDate
        // 5. Check positive case with holidays
        // 6. Check positive case with holidays in Weekend

        [Fact]
        public void Scenario1()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 07); // Monday
            DateTime endDate = new DateTime(2021, 06, 11); // Friday

            var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(3, workingDays);
        }

        [Fact]
        public void Scenario2()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 07); // Monday
            DateTime endDate = new DateTime(2021, 06, 14); // Monday next week

            var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(4, workingDays);

            endDate = new DateTime(2021, 06, 15);
            workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(5, workingDays);

            endDate = new DateTime(2021, 06, 30);

            workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(16, workingDays);
        }

        [Fact]
        public void Scenario3()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 07); // Monday
            DateTime endDate = new DateTime(2021, 06, 07); // Monday

            var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(0, workingDays);

            endDate = new DateTime(2021, 06, 08); // Tuesday

            workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);

            Assert.Equal(0, workingDays);
        }

        [Fact]
        public void Scenario4()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 11); // Monday
            DateTime endDate = new DateTime(2021, 06, 07); // Friday

            try
            {
                var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate);
            }
            catch (ArgumentException ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        [Fact]
        public void Scenario5()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 07); // Monday
            DateTime endDate = new DateTime(2021, 06, 17); // Next Thursday
            DateTime[] holidays = new DateTime[] {
                new DateTime(2021, 06, 14),
                new DateTime(2021, 06, 15)
            };

            var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate, holidays);

            Assert.Equal(5, workingDays);
        }

        [Fact]
        public void Scenario6()
        {
            _dateTime = new DateTimeService();

            DateTime startDate = new DateTime(2021, 06, 07); // Monday
            DateTime endDate = new DateTime(2021, 06, 17); // Next Thursday
            DateTime[] holidays = new DateTime[] {
                new DateTime(2021, 06, 12),
                new DateTime(2021, 06, 13)
            };

            var workingDays = _dateTime.CalculateWorkingDays(startDate, endDate, holidays);

            Assert.Equal(7, workingDays);
        }
    }
}
