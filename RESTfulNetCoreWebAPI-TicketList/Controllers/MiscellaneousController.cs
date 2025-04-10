﻿using Microsoft.AspNetCore.Mvc;
using RESTfulNetCoreWebAPI_TicketList.Services;

namespace RESTfulNetCoreWebAPI_TicketList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscellaneousController : ControllerBase
    {
        private readonly IMiscellaneousService _miscellaneousService;

        public MiscellaneousController(IMiscellaneousService miscellaneousService)
        {
            _miscellaneousService = miscellaneousService;
        }

        // GET api/miscellaneous/getFibonacciSequence/90
        [HttpGet("getFibonacciSequence/{maxSequence}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetFibonacciSequence([FromRoute] int maxSequence)
        {
            try
            {
                var fibonacciSequence = _miscellaneousService.GetFibonacciSequence(maxSequence);
                return Ok(fibonacciSequence);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET api/miscellaneous/getPalindromeWords
        [HttpPost("getPalindromeWords")]
        public IActionResult GetPalindromeWords([FromBody] string[]? words)
        {
            try
            {
                return Ok(_miscellaneousService.GetPalindromeWords(words));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Description", e.Message);
                return ValidationProblem(ModelState);
            }
        }
    }
}
