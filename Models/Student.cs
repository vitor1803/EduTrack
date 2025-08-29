using api.Dtos;

namespace api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public List<Enroll> Enrolls { get; set; } = new();

        // Relacionamento 1:1 com AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public Student() { }

        public Student(string Name, string appUserId)
        {
            this.Name = Name;
            this.AppUserId = appUserId;
        }

        public DTOStudentGetInfo ToDTO()
        {
            return new DTOStudentGetInfo(this.Id, this.Name);
        }
    }
}