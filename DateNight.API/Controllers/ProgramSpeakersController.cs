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
    public class ProgramSpeakersController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public ProgramSpeakersController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/programspeakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramSpeakerDto>>> GetAllProgramSpeakers()
        {
            var programSpeakers = await dbContext.ProgramSpeaker
                .Include(ps => ps.Program)
                .Include(ps => ps.Speaker)
                .Select(ps => new ProgramSpeakerDto
                {
                    ProgramSpeakerId = ps.ProgramSpeakerId,
                    ProgramId = ps.ProgramId,
                    SpeakerId = ps.SpeakerId,
                    Program = new ProgramDto
                    {
                        ProgramId = ps.Program.ProgramId,
                        ProgramName = ps.Program.ProgramName,
                        ProgramDescription = ps.Program.ProgramDescription,
                        StartTime = ps.Program.StartTime,
                        EndTime = ps.Program.EndTime
                    },
                    Speaker = new SpeakerDto
                    {
                        SpeakerId = ps.Speaker.SpeakerId,
                        SpeakerName = ps.Speaker.SpeakerName,
                        SpeakerBio = ps.Speaker.SpeakerBio
                    }
                })
                .ToListAsync();

            return Ok(programSpeakers);
        }

        // GET: api/programspeakers/{id}
        [HttpGet("getPsById/{id}", Name = "GetProgramSpeakerById")]
        public async Task<ActionResult<ProgramSpeakerDto>> GetProgramSpeakerById(Guid id)
        {
            var programSpeaker = await dbContext.ProgramSpeaker
                .Include(ps => ps.Program)
                .Include(ps => ps.Speaker)
                .FirstOrDefaultAsync(ps => ps.ProgramSpeakerId == id);

            if (programSpeaker == null)
            {
                return NotFound();
            }

            var programSpeakerDto = new ProgramSpeakerDto
            {
                ProgramSpeakerId = programSpeaker.ProgramSpeakerId,
                ProgramId = programSpeaker.ProgramId,
                SpeakerId = programSpeaker.SpeakerId,
                Program = new ProgramDto
                {
                    ProgramId = programSpeaker.Program.ProgramId,
                    ProgramName = programSpeaker.Program.ProgramName,
                    ProgramDescription = programSpeaker.Program.ProgramDescription,
                    StartTime = programSpeaker.Program.StartTime,
                    EndTime = programSpeaker.Program.EndTime
                },
                Speaker = new SpeakerDto
                {
                    SpeakerId = programSpeaker.Speaker.SpeakerId,
                    SpeakerName = programSpeaker.Speaker.SpeakerName,
                    SpeakerBio = programSpeaker.Speaker.SpeakerBio
                }
            };

            return Ok(programSpeakerDto);
        }

        // POST: api/programspeakers
        [HttpPost("addPS")]
        public async Task<ActionResult<ProgramSpeakerDto>> CreateProgramSpeaker(AddProgramSpeakerRequestDto addProgramSpeakerDto)
        {
            var newProgramSpeaker = new ProgramSpeaker
            {
                ProgramSpeakerId = Guid.NewGuid(),
                ProgramId = addProgramSpeakerDto.ProgramId,
                SpeakerId = addProgramSpeakerDto.SpeakerId
            };

            dbContext.ProgramSpeaker.Add(newProgramSpeaker);
            await dbContext.SaveChangesAsync();

            var programSpeakerDto = new ProgramSpeakerDto
            {
                ProgramSpeakerId = newProgramSpeaker.ProgramSpeakerId,
                ProgramId = newProgramSpeaker.ProgramId,
                SpeakerId = newProgramSpeaker.SpeakerId
            };

            return CreatedAtRoute("GetProgramSpeakerById", new { id = programSpeakerDto.ProgramSpeakerId }, programSpeakerDto);
        }

        // PUT: api/programspeakers/{id}
        [HttpPut("updatePS/{id}")]
        public async Task<IActionResult> UpdateProgramSpeaker(Guid id, UpdateProgramSpeakerRequestDto updateProgramSpeakerDto)
        {
            if (id != updateProgramSpeakerDto.ProgramSpeakerId)
            {
                return BadRequest();
            }

            var programSpeaker = await dbContext.ProgramSpeaker.FindAsync(id);

            if (programSpeaker == null)
            {
                return NotFound();
            }

            programSpeaker.ProgramId = updateProgramSpeakerDto.ProgramId;
            programSpeaker.SpeakerId = updateProgramSpeakerDto.SpeakerId;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramSpeakerExists(id))
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

        // DELETE: api/programspeakers/{id}
        [HttpDelete("deletePS/{id}")]
        public async Task<IActionResult> DeleteProgramSpeaker(Guid id)
        {
            var programSpeaker = await dbContext.ProgramSpeaker.FindAsync(id);
            if (programSpeaker == null)
            {
                return NotFound();
            }

            dbContext.ProgramSpeaker.Remove(programSpeaker);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramSpeakerExists(Guid id)
        {
            return dbContext.ProgramSpeaker.Any(ps => ps.ProgramSpeakerId == id);
        }
    }
}
