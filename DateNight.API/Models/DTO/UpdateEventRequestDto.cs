namespace DateNight.API.Models.DTO
{
    public class UpdateEventRequestDto
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
    }

}
