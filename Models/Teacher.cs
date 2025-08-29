using api.Dtos;

namespace api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Course> Courses { get; set; } = new();

        // Relacionamento 1:1 com AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public DTOTeacherGetInfo ToDTO()
        {
            return new DTOTeacherGetInfo(this.Id, this.Name, this.Courses.Select(s => s.ToDTOCourse()).ToList());
        }

        public Teacher() { }

        public Teacher(DTOTeacherCreate dtoTeacherCreate)
        {
            this.Name = dtoTeacherCreate.Name;
        }

        public Teacher(DTOSingUpTeacher dto, AppUser appUser)
        {
            this.Name = dto.Name;
            this.AppUserId = appUser.Id;
        }

        public Teacher(string Name, string appUserId)
        {
            this.Name = Name;
            this.AppUserId = appUserId;
        }

    }
}