using DateNight.API.Data;
using DateNight.API.Models.Domain;
using DateNight.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public SemesterController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllSemester() 
        {
            var semestersDomain = dbContext.Semesters.ToList();

            var semestersDto = new List<SemesterDto>();
            foreach (var item in semestersDomain)
            {
                semestersDto.Add(new SemesterDto()
                {
                    Id = item.Id,
                    SemesterName = item.SemesterName
                });
            }

            return Ok(semestersDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetSemesterById([FromRoute]Guid id)
        {
            var semesterDomain = dbContext.Semesters.FirstOrDefault(x => x.Id == id);

            if (semesterDomain == null)
            {
                return BadRequest();
            }

            var semesterDto = new SemesterDto
            {
                Id = semesterDomain.Id,
                SemesterName = semesterDomain.SemesterName
            };

            return Ok(semesterDto);
        }

        [HttpPost]
        public IActionResult CreateSemester([FromBody] AddSemesterRequestDto addSemesterRequestDto)
        {
            var semesterDomainModel = new Semester
            {
                SemesterName = addSemesterRequestDto.SemesterName,
            };

            dbContext.Semesters.Add(semesterDomainModel);
            dbContext.SaveChanges();

            var semesterDto = new SemesterDto
            {
                Id = semesterDomainModel.Id,
                SemesterName = semesterDomainModel.SemesterName
            };

            return CreatedAtAction(nameof(GetSemesterById), new {id = semesterDto.Id }, semesterDto);
        }
    }
}
