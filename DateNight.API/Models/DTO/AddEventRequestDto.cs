namespace DateNight.API.Models.DTO
{
    public class AddEventRequestDto
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
    }

}
