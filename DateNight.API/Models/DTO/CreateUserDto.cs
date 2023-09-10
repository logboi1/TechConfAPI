namespace DateNight.API.Models.DTO
{
    public class CreateUserDto
    {
     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string City { get; set; }
        public String? UserImageUrl { get; set; }
        public Guid LevelId { get; set; }
        public Guid SemesterId { get; set; }
    }
}
