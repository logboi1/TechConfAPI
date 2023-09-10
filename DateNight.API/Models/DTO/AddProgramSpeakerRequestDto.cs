namespace DateNight.API.Models.DTO
{
    public class AddProgramSpeakerRequestDto
    {
        public Guid ProgramId { get; set; }
        public Guid SpeakerId { get; set; }
    }
}
