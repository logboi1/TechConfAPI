using DateNight.API.Data;
using DateNight.API.Models.Domain;
using DateNight.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DateNight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public SpeakersController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/speakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerDto>>> GetAllSpeakers()
        {
            var speakers = await dbContext.SpeakerSpeaker
                .Select(s => new SpeakerDto
                {
                    SpeakerId = s.SpeakerId,
                    SpeakerName = s.SpeakerName,
                    SpeakerBio = s.SpeakerBio
                })
                .ToListAsync();

            return Ok(speakers);
        }

        // GET: api/speakers/{id}
        [HttpGet("{id}", Name = "GetSpeakerById")]
        public async Task<ActionResult<SpeakerDto>> GetSpeakerById(Guid id)
        {
            var speaker = await dbContext.SpeakerSpeaker.FindAsync(id);

            if (speaker == null)
            {
                return NotFound();
            }

            var speakerDto = new SpeakerDto
            {
                SpeakerId = speaker.SpeakerId,
                SpeakerName = speaker.SpeakerName,
                SpeakerBio = speaker.SpeakerBio
            };

            return Ok(speakerDto);
        }

        // POST: api/speakers
        [HttpPost]
        public async Task<ActionResult<SpeakerDto>> CreateSpeaker(AddSpeakerRequestDto addSpeakerDto)
        {
            var newSpeaker = new Speaker
            {
                SpeakerId = Guid.NewGuid(),
                SpeakerName = addSpeakerDto.SpeakerName,
                SpeakerBio = addSpeakerDto.SpeakerBio
            };

            dbContext.SpeakerSpeaker.Add(newSpeaker);
            await dbContext.SaveChangesAsync();

            var speakerDto = new SpeakerDto
            {
                SpeakerId = newSpeaker.SpeakerId,
                SpeakerName = newSpeaker.SpeakerName,
                SpeakerBio = newSpeaker.SpeakerBio
            };

            return CreatedAtRoute("GetSpeakerById", new { id = speakerDto.SpeakerId }, speakerDto);
        }

        // PUT: api/speakers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpeaker(Guid id, UpdateSpeakerRequestDto updateSpeakerDto)
        {
            if (id != updateSpeakerDto.SpeakerId)
            {
                return BadRequest();
            }

            var speaker = await dbContext.SpeakerSpeaker.FindAsync(id);

            if (speaker == null)
            {
                return NotFound();
            }

            speaker.SpeakerName = updateSpeakerDto.SpeakerName;
            speaker.SpeakerBio = updateSpeakerDto.SpeakerBio;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // DELETE: api/speakers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeaker(Guid id)
        {
            var speaker = await dbContext.SpeakerSpeaker.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }

            dbContext.SpeakerSpeaker.Remove(speaker);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool SpeakerExists(Guid id)
        {
            return dbContext.SpeakerSpeaker.Any(s => s.SpeakerId == id);
        }
    }
}
