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

        // GET api/tickets/getAll
        [HttpGet("getAll")]
        public IActionResult GetAllTickets()
        {
            var ticketList = _ticketService.GetTickets();

            return Ok(ticketList);
        }

        // GET api/tickets/1
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetTicketById([FromRoute] int id)
        {
            try
            {
                var ticket = _ticketService.GetTicket(id);

                if (ticket == null)
                {
                    return NotFound();
                }

                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        // POST api/tickets
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] TicketInputDTO? ticketItem)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(ticketItem);

                await _ticketService.AddTicketAsync(ticket);

                return CreatedAtRoute("GetTicket", new { id = ticket?.Id }, ticket);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        // PATCH api/tickets/2
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTicket([FromRoute] int id, [FromBody] JsonPatchDocument<Ticket> ticketItem)
        {
            try
            {
                var ticket = _ticketService.GetTicket(id);
                if (ticket == null)
                {
                    return NotFound();
                }

                ticketItem.ApplyTo(ticket);

                await _ticketService.PatchTicketAsync(ticket);

                return Ok(ticket);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/ticket/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            try
            {
                await _ticketService.DeleteTicketAsync(id);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
