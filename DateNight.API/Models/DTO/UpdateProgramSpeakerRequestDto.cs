namespace DateNight.API.Models.DTO
{
    public class UpdateProgramSpeakerRequestDto
    {
        public Guid ProgramSpeakerId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid SpeakerId { get; set; }
    }
}
