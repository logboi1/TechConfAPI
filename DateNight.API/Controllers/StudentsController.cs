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
    public class StudentsController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public StudentsController(DateNightDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
           var StudentsDomain = dbContext.Users.ToList();

            var studentsDto = new List<StudentDto>();
            foreach (var student in StudentsDomain)
            {
                studentsDto.Add(new StudentDto()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Bio = student.Bio,
                    City = student.City,
                    UserImageUrl = student.UserImageUrl,
                    LevelId = student.LevelId,
                    SemesterId = student.SemesterId,
                    Level = student.Level,
                    Semester =  student.Semester,
                });
            }

            return Ok(studentsDto);
        }


        // get a single student detail

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetStudentById([FromRoute]Guid id, string levelName) 
        {
            var studentDomain = dbContext.Users
                .Include(u => u.Semester)
                .Include(u=>u.Level)
                .FirstOrDefault(x => x.Id == id);

            if (studentDomain == null)
            {
                return NotFound();
            }

            var studentDto = new StudentDto
            {
                Id = studentDomain.Id,
                FirstName = studentDomain.FirstName,
                LastName = studentDomain.LastName,
                Bio = studentDomain.Bio,
                City = studentDomain.City,
                UserImageUrl = studentDomain.UserImageUrl,
              //  LevelId = studentDomain.LevelId,
              //  SemesterId = studentDomain.SemesterId,
                Level = studentDomain.Level,
                Semester = studentDomain.Semester,
            };

            return Ok(studentDto);
        }

        //to creae user

        [HttpPost]

        public IActionResult CreateStudent([FromBody] CreateUserDto createUserDto )
        {
            if (createUserDto == null)
            {
                return BadRequest("Invalid data received. Please provide valid input");
            }

            var newUser = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Bio = createUserDto.Bio,
                City = createUserDto.City,
                UserImageUrl = createUserDto.UserImageUrl,
                LevelId = createUserDto.LevelId,
                SemesterId = createUserDto.SemesterId,
            };

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            var studentDto = new StudentDto
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Bio = newUser.Bio,
                City = newUser.City,
                UserImageUrl = newUser.UserImageUrl,
                LevelId = newUser.LevelId,
                SemesterId = newUser.SemesterId,
            };

            return CreatedAtAction("GetStudentById", new { id = studentDto.Id }, studentDto);
        }
    }
}
