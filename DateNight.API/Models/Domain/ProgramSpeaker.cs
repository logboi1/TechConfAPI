namespace DateNight.API.Models.Domain
{
    public class ProgramSpeaker
    {
        public Guid ProgramSpeakerId { get; set; } 
        public Guid ProgramId { get; set; }         // Foreign Key, references programs(program_id)
        public Guid SpeakerId { get; set; }         // Foreign Key, references speakers(speaker_id)

        // Navigation properties for the associated program and speaker
        public Programs Program { get; set; }
        public Speaker Speaker { get; set; }
    }
}
