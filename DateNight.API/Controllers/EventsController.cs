using DateNight.API.Data;
using DateNight.API.Models.DTO;
using DateNight.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Logging;

namespace DateNight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public EventsController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //GET: api/events
        [HttpGet("getAllEvents", Name ="GetAllEvents")]
        public async Task<ActionResult<IEnumerable<EventDto>>>GetAllEvents()
        {
            var events = await dbContext.Events
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    EventName = e.EventName,
                    EventDescription = e.EventDescription,
                    EventDate = e.EventDate
                })
                .ToListAsync();

            return Ok(events);
        }


        // GET: api/events/{id}
        [HttpGet("getEventById/{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDto>>GetEventById(Guid id)
        {
            var @event = await dbContext.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            var eventDto = new EventDto
            {
                EventId = @event.EventId,
                EventName = @event.EventName,
                EventDescription = @event.EventDescription,
                EventDate = @event.EventDate
            };

            return Ok(eventDto);
        }


        // GET: api/events/{eventName}
        [HttpGet("getEventByName/{eventName}", Name = "GetEventByName")]
        public async Task<ActionResult<EventDto>> GetEventByName(string eventName)
        {
            var @event = await dbContext.Events
                .FirstOrDefaultAsync(e => e.EventName == eventName);

            if (@event == null)
            {
                return NotFound();
            }

            var eventDto = new EventDto
            {
                EventId = @event.EventId,
                EventName = @event.EventName,
                EventDescription = @event.EventDescription,
                EventDate = @event.EventDate
            };

            return Ok(eventDto);
        }

        // POST: api/events
        [HttpPost("createEvent", Name ="CreateEvent")]
        public async Task<ActionResult<EventDto>> CreateEvent(AddEventRequestDto addEventDto)
        {
            // Create a new event entity from the DTO
            var newEvent = new Events
            {
                EventName = addEventDto.EventName,
                EventDescription = addEventDto.EventDescription,
                EventDate = addEventDto.EventDate
            };

            // Add the new event to the database
            dbContext.Events.Add(newEvent);
            await dbContext.SaveChangesAsync();

            // Create a DTO for the newly created event
            var eventDto = new EventDto
            {
                EventId = newEvent.EventId,
                EventName = newEvent.EventName,
                EventDescription = newEvent.EventDescription,
                EventDate = newEvent.EventDate
            };

            return CreatedAtRoute("GetEventById", new { id = eventDto.EventId }, eventDto);
        }



        // PUT: api/events/{id}
        [HttpPut("updateEvent/{id}", Name ="UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventRequestDto updateEventDto)
        {
            if (id != updateEventDto.EventId)
            {
                return BadRequest();
            }

            var @event = await dbContext.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            // Update the event properties
            @event.EventName = updateEventDto.EventName;
            @event.EventDescription = updateEventDto.EventDescription;
            @event.EventDate = updateEventDto.EventDate;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/events/{id}
        [HttpDelete("deleteEvent/{id}", Name ="DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await dbContext.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            dbContext.Events.Remove(@event);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

      


    }
}
