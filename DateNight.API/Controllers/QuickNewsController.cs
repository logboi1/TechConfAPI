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
    public class QuickNewsController : ControllerBase
    {
        private readonly DateNightDbContext dbContext;
        public QuickNewsController(DateNightDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Get a list of all quick news articles.
        /// </summary>
        /// <remarks>
        /// GET: api/quicknews
        /// This endpoint retrieves a list of quick news articles.
        /// </remarks>


        // GET: api/quicknews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuickNewsDto>>> GetAllQuickNews()
        {
            var quickNews = await dbContext.QuickNews
                .Select(qn => new QuickNewsDto
                {
                    NewsId = qn.NewsId,
                    Title = qn.Title,
                    Content = qn.Content,
                    PublishDate = qn.PublishDate,
                    Author = qn.Author
                })
                .ToListAsync();

            return Ok(quickNews);
        }

        // GET: api/quicknews/{id}
        [HttpGet("{id}", Name = "GetQuickNewsById")]
        public async Task<ActionResult<QuickNewsDto>> GetQuickNewsById(int id)
        {
            var quickNews = await dbContext.QuickNews.FindAsync(id);

            if (quickNews == null)
            {
                return NotFound();
            }

            var quickNewsDto = new QuickNewsDto
            {
                NewsId = quickNews.NewsId,
                Title = quickNews.Title,
                Content = quickNews.Content,
                PublishDate = quickNews.PublishDate,
                Author = quickNews.Author
            };

            return Ok(quickNewsDto);
        }

        // POST: api/quicknews
        [HttpPost("addQuickNews")]
        public async Task<ActionResult<QuickNewsDto>> CreateQuickNews(CreateQuickNewsRequestDto createQuickNewsDto)
        {
            var newQuickNews = new QuickNews
            {
                Title = createQuickNewsDto.Title,
                Content = createQuickNewsDto.Content,
                PublishDate = createQuickNewsDto.PublishDate,
                Author = createQuickNewsDto.Author
            };

            dbContext.QuickNews.Add(newQuickNews);
            await dbContext.SaveChangesAsync();

            var quickNewsDto = new QuickNewsDto
            {
                NewsId = newQuickNews.NewsId,
                Title = newQuickNews.Title,
                Content = newQuickNews.Content,
                PublishDate = newQuickNews.PublishDate,
                Author = newQuickNews.Author
            };

            return CreatedAtRoute("GetQuickNewsById", new { id = quickNewsDto.NewsId }, quickNewsDto);
        }

        // PUT: api/quicknews/{id}
        [HttpPut("updateQuickNews/{id}")]
        public async Task<IActionResult> UpdateQuickNews(int id, UpdateQuickNewsRequestDto updateQuickNewsDto)
        {
            if (id != updateQuickNewsDto.NewId)
            {
                return BadRequest();
            }

            var quickNews = await dbContext.QuickNews.FindAsync(id);

            if (quickNews == null)
            {
                return NotFound();
            }

            quickNews.Title = updateQuickNewsDto.Title;
            quickNews.Content = updateQuickNewsDto.Content;
            quickNews.PublishDate = updateQuickNewsDto.PublishDate;
            quickNews.Author = updateQuickNewsDto.Author;

            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/quicknews/{id}
        [HttpDelete("deleteQuickNews/{id}")]
        public async Task<IActionResult> DeleteQuickNews(int id)
        {
            var quickNews = await dbContext.QuickNews.FindAsync(id);
            if (quickNews == null)
            {
                return NotFound();
            }

            dbContext.QuickNews.Remove(quickNews);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
