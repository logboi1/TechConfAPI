namespace DateNight.API.Models.DTO
{
    public class ProgramSpeakerDto
    {
        public Guid ProgramSpeakerId { get; set; }
        public Guid ProgramId { get; set; }
        public Guid SpeakerId { get; set; }
        public ProgramDto Program { get; set; }
        public SpeakerDto Speaker { get; set; }
    }
}
