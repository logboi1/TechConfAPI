using System.ComponentModel.DataAnnotations;

namespace DateNight.API.Models.Domain
{
    public class Events
    {
        [Key]
        public Guid EventId { get; set; }          
        public string EventName { get; set; }      
        public string EventDescription { get; set; } 
        public DateTime EventDate { get; set; }
    }
}
