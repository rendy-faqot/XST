using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XST.Api.Features.DateTimeCalculator.Query;

namespace XST.Api.Features.DateTimeCalculator
{
    [Route("CalculateWorkingDays")]
    public class DateTimeCalculatorController : Controller
    {
        private readonly IMediator _mediator;

        public DateTimeCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]CalculateWorkingDays request)
        {
            try
            {
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
