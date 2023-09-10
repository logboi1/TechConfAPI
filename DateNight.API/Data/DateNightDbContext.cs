using DateNight.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DateNight.API.Data
{
    public class DateNightDbContext: DbContext
    {
        public DateNightDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<ProgramSpeaker> ProgramSpeaker { get; set; }

        public DbSet<Speaker> SpeakerSpeaker { get; set;}

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<Programs> Programs { get; set; }

        public DbSet<QuickNews> QuickNews { get; set; }

    }
}
