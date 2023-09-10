using System.ComponentModel.DataAnnotations;

namespace DateNight.API.Models.Domain
{
    public class Programs
    {
             [Key]
            public Guid ProgramId { get; set; }              // Primary Key, AUTO_INCREMENT in the database
            public Guid EventId { get; set; }                // Foreign Key, references events(event_id)
            public string ProgramName { get; set; }         // VARCHAR(255) NOT NULL
            public string ProgramDescription { get; set; }  // TEXT
            public TimeSpan StartTime { get; set; }         // TIME NOT NULL
            public TimeSpan EndTime { get; set; }           // TIME NOT NULL

            // Navigation property for the associated event
            public Events Event { get; set; }
  
    }
}
