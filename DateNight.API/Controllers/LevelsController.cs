using DateNight.API.Data;
using DateNight.API.Models.Domain;
using DateNight.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateNight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;

        public LevelsController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all Levels
        [HttpGet]
        public IActionResult GetAll() 
        {
            var levelsDomain = dbContext.Levels.ToList();

            //Map Domain to DTO
            var levelsDto = new List<LevelDto>();
            foreach (var levelDomain in levelsDomain)
            {
                levelsDto.Add(new LevelDto()
                {
                    Id = levelDomain.Id,
                    LevelName = levelDomain.LevelName
                });
            }
            return Ok(levelsDomain);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            // var level = dbContext.Levels.Find(id);

            var levelDomain = dbContext.Levels.FirstOrDefault(x => x.Id == id);


            if (levelDomain == null)
            {
                return NotFound();
            }


            var levelDto = new LevelDto
            {
                Id = levelDomain.Id,
                LevelName = levelDomain.LevelName
            };

            return Ok(levelDto);
        }


        //Post: to create
        [HttpPost]
        public IActionResult Create([FromBody] AddLevelRequestDto addLevelRequestDto)
        {
            var levelDomainModel = new Level
            {
                LevelName = addLevelRequestDto.LevelName,
            };

            dbContext.Levels.Add(levelDomainModel);
            dbContext.SaveChanges();

            var levelDto = new LevelDto
            {
                Id = levelDomainModel.Id,
                LevelName = levelDomainModel.LevelName
            };

            return CreatedAtAction(nameof(GetById), new { id = levelDto.Id }, levelDto);
        }
    }
}
