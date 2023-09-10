namespace DateNight.API.Models.DTO
{
    public class UpdateSpeakerRequestDto
    {
        public Guid SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
    }
}
