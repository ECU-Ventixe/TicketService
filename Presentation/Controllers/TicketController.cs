using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Models;
using Presentation.Service;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketService ticketService) : ControllerBase
    {
        private readonly TicketService _ticketService = ticketService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TicketDto ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null");
            }

            var createdTickets = new List<TicketEntity>();

            for (int i = 0; i < ticket.TicketQuantity; i++)
            {
                var ticketEntity = new TicketEntity
                {
                    EventId = ticket.EventId,
                    UserId = ticket.UserId,
                    FirstName = ticket.FirstName,
                    LastName = ticket.LastName,
                    Email = ticket.Email,
                    PhoneNumber = ticket.PhoneNumber,
                    PostalCode = ticket.PostalCode,
                    Address = ticket.Address
                };
                var createdTicket = await _ticketService.Create(ticketEntity);
                createdTickets.Add(createdTicket);
            }
            return Ok(createdTickets);
        }
        [HttpGet("getticket/{Id}")]
        public async Task<IActionResult> GetTickets(string id)
        {
            var tickets = await _ticketService.GetByUserId(id);
            return Ok(tickets);
        }
        [HttpGet("geteventtickets/{id}")]
        public async Task<IActionResult> GetEventTickets(string id)
        {
            var eventTickets = await _ticketService.GetTicket();
            return Ok(eventTickets);
        }
        [HttpGet("bought/{id}")]
        public async Task<IActionResult> GetBoughtTickets(string id)
        {
            var tickets = await _ticketService.GetByEventId(id);
            if (tickets == null || !tickets.Any())
            {
                return NotFound($"No tickets found for EventId {id}.");
            }
            
            return Ok(tickets.Count);
        }
    }
}
