namespace DateNight.API.Models.DTO
{
    public class QuickNewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
    }
}
