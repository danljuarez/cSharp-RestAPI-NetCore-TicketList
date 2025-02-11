using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetFiboSequence([FromRoute] int maxSequence)
        {
            try
            {
                return Ok(_miscellaneousService.FibonacciList(maxSequence));
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
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
