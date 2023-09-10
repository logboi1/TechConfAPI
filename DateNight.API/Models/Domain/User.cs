namespace DateNight.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string City { get; set; }
        public String? UserImageUrl { get; set; }
        public Guid LevelId { get; set; }
        public Guid SemesterId { get; set; }

        //Navigation properties
        public Level Level { get; set; }
        public Semester Semester { get; set; }
    }
}
