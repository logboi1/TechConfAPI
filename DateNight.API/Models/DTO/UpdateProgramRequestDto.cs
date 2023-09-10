namespace DateNight.API.Models.DTO
{
    public class UpdateProgramRequestDto
    {
        public Guid ProgramId { get; set; }
        public Guid EventId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
