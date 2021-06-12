using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XST.Service.Interfaces;

namespace XST.Api.Features.DateTimeCalculator.Query
{

    public class CalculateWorkingDays : IRequest<int>
    {
        public string StartDt { get; set; }
        public string EndDt { get; set; }
    }

    public class CalculateWorkingDaysHandler : IRequestHandler<CalculateWorkingDays, int>
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTime _dateTimeService;

        public CalculateWorkingDaysHandler(IDateTime dateTimeService, IConfiguration configuration)
        {
            _dateTimeService = dateTimeService;
            _configuration = configuration;
        }

        public async Task<int> Handle(CalculateWorkingDays request, CancellationToken cancellationToken)
        {
            try
            {
                List<DateTime> listHoliday = new List<DateTime>();
                var holidays = _configuration.GetSection("Holidays").GetChildren().Select(x => x.Value).ToArray();
                for (int iterator = 0; iterator < holidays.Length; iterator++)
                {
                    DateTime daysOff = DateTime.Parse(holidays[iterator]);
                    listHoliday.Add(daysOff);
                }

                DateTime startDt = DateTime.Parse(request.StartDt);
                DateTime endDt = DateTime.Parse(request.EndDt);

                return await Task.Run(() => _dateTimeService.CalculateWorkingDays(startDt, endDt, listHoliday));
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
