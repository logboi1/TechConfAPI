using System.ComponentModel.DataAnnotations;

namespace DateNight.API.Models.DTO
{
    public class UpdateQuickNewsRequestDto
    {
        [Required]
        public int NewId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
    }
}
