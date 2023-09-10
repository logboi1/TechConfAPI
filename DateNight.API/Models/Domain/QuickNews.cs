using System.ComponentModel.DataAnnotations;

namespace DateNight.API.Models.Domain
{
    public class QuickNews
    {
        [Key]
        public int NewsId { get; set; }

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
