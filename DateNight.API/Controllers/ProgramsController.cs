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
    public class ProgramsController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public ProgramsController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET: api/programs
        [HttpGet("getAllPrograms")]

        public async Task<ActionResult<IEnumerable<ProgramDto>>>GetAllPrograms()
        {
            var programs = await dbContext.Programs
                .Select(p => new ProgramDto
                {
                    ProgramId = p.ProgramId,
                    EventId = p.EventId,
                    ProgramName = p.ProgramName,
                    ProgramDescription = p.ProgramDescription,
                    StartTime = p.StartTime,
                    EndTime = p.EndTime,
                })
                .ToListAsync();

            return Ok(programs);
        }

        // GET: api/programs/{id}
        [HttpGet("getProgramById/{id}", Name = "GetProgramById")]
        public async Task<ActionResult<ProgramDto>> GetProgramById(Guid id)
        {
            var program = await dbContext.Programs.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            var programDto = new ProgramDto
            {
                ProgramId = program.ProgramId,
                EventId = program.EventId,
                ProgramName = program.ProgramName,
                ProgramDescription = program.ProgramDescription,
                StartTime = program.StartTime,
                EndTime = program.EndTime
            };

            return Ok(programDto);
        }

        // GET: api/programs/byevent/{eventId}
        [HttpGet("getProgramsByEvent/{eventId}")]
        public async Task<ActionResult<IEnumerable<ProgramDto>>> GetProgramsByEventId(Guid eventId)
        {
            var programs = await dbContext.Programs
                .Where(p => p.EventId == eventId)
                .Select(p => new ProgramDto
                {
                    ProgramId = p.ProgramId,
                    EventId = p.EventId,
                    ProgramName = p.ProgramName,
                    ProgramDescription = p.ProgramDescription,
                    StartTime = p.StartTime,
                    EndTime = p.EndTime
                })
                .ToListAsync();

            if (programs == null || programs.Count == 0)
            {
                return NotFound();
            }

            return Ok(programs);
        }


        // POST: api/programs
        [HttpPost("addProgram", Name ="AddNewProgram")]
        public async Task<ActionResult<ProgramDto>> AddProgram(AddProgramRequestDto addProgramDto)
        {
            var newProgram = new Programs
            {
              //  ProgramId = Guid.NewGuid(),
                EventId = addProgramDto.EventId,
                ProgramName = addProgramDto.ProgramName,
                ProgramDescription = addProgramDto.ProgramDescription,
                StartTime = addProgramDto.StartTime,
                EndTime = addProgramDto.EndTime
            };

            dbContext.Programs.Add(newProgram);
            await dbContext.SaveChangesAsync();

            var programDto = new ProgramDto
            {
                ProgramId = newProgram.ProgramId,
                EventId = newProgram.EventId,
                ProgramName = newProgram.ProgramName,
                ProgramDescription = newProgram.ProgramDescription,
                StartTime = newProgram.StartTime,
                EndTime = newProgram.EndTime
            };

            return CreatedAtRoute("GetProgramById", new { id = programDto.ProgramId }, programDto);
        }

        // PUT: api/programs/{id}
        [HttpPut("updateProgram/{id}")]
        public async Task<IActionResult> UpdateProgram(Guid id, UpdateProgramRequestDto updateProgramDto)
        {
            if (id != updateProgramDto.ProgramId)
            {
                return BadRequest();
            }

            var program = await dbContext.Programs.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            program.EventId = updateProgramDto.EventId;
            program.ProgramName = updateProgramDto.ProgramName;
            program.ProgramDescription = updateProgramDto.ProgramDescription;
            program.StartTime = updateProgramDto.StartTime;
            program.EndTime = updateProgramDto.EndTime;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramExists(id))
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

        // DELETE: api/programs/{id}
        [HttpDelete("deleteProgram/{id}")]
        public async Task<IActionResult> DeleteProgram(Guid id)
        {
            var program = await dbContext.Programs.FindAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            dbContext.Programs.Remove(program);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramExists(Guid id)
        {
            return dbContext.Programs.Any(p => p.ProgramId == id);
        }

    }
}
