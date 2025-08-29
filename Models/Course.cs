using api.Dtos;

namespace api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = new();
    
        public List<Enroll> Enrolls { get; set; } = new();

        public Course() { }

        public Course(DTOCourseCreate dto, Teacher teacher)
        {
            this.Name = dto.Name;
            this.TeacherId = dto.TeacherId;
            this.Teacher = teacher;
        }

        public DTOCourseGetInfo ToDTOCourse()
        {
            return new DTOCourseGetInfo(this.Id, this.Name, this.Enrolls.Select(e => e.Student!.ToDTO()).ToList());
        }
    }
}