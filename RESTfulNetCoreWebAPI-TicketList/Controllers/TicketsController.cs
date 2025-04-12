using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTfulNetCoreWebAPI_TicketList.Models;
using RESTfulNetCoreWebAPI_TicketList.Models.Request;
using RESTfulNetCoreWebAPI_TicketList.Services;

namespace RESTfulNetCoreWebAPI_TicketList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public TicketsController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper; // Use AutoMapper for handling DTOs
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Ticket>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTicketsAsync()
        {
            var ticketList = await _ticketService.GetTicketsAsync();
            return Ok(ticketList);
        }

        // GET api/tickets/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
        public IActionResult GetTicketById([FromRoute] int id)
        {
            try
            {
                var ticket = _ticketService.GetTicket(id);
                return ticket != null ? Ok(ticket) : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/tickets
        [HttpPost]
        [ProducesResponseType(typeof(Ticket), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostTicket([FromBody] TicketInputDTO? ticketItem)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(ticketItem);
                var result = await _ticketService.AddTicketAsync(ticket);
                return CreatedAtAction(nameof(GetTicketById), new { id = result?.Id }, result);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH api/tickets/2
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
        public async Task<IActionResult> PatchTicket([FromRoute] int id, [FromBody] JsonPatchDocument<Ticket> ticketItem)
        {
            try
            {
                var ticket = _ticketService.GetTicket(id);
                if (ticket == null) return NotFound();

                ticketItem.ApplyTo(ticket);
                await _ticketService.PatchTicketAsync(ticket);
                return Ok(ticket);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/ticket/3
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
