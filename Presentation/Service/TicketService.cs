using Microsoft.EntityFrameworkCore;
using Presentation.Data;

namespace Presentation.Service
{
    public class TicketService(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task<TicketEntity> Create(TicketEntity ticket)
        {

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }
        public async Task<List<TicketEntity>> GetTicket()
        {
            return await _context.Tickets.ToListAsync();
        }
        public async Task<List<TicketEntity>> GetByUserId(string id)
        {
            var ticket = _context.Tickets
                .Where(t => t.UserId == id)
                .ToList();
            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with EventId {id} not found.");
            }

            return ticket;

        }
        public async Task<List<TicketEntity>> GetByEventId(string id)
        {
            var ticket = _context.Tickets
                .Where(t => t.EventId == id)
                .ToList();
            
            if (ticket == null)
            {
                throw new KeyNotFoundException($"Ticket with Id {id} not found.");
            }
            return ticket;
        }
    }
}
