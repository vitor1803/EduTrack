using api.Dtos;

namespace api.Models
{
    public class Enroll
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; } = null;

        public int StudentId { get; set; }
        public Student? Student { get; set; } = null;

        public int? FinalGrade { get; set; }

        public Enroll(){ }
        public Enroll(DTOEnrollCreate dto)
        {
            this.CourseId = dto.CourseId;
            this.StudentId = dto.StudentId;
        }

        public DTOEnrollGetInfo ToDTO()
        {
            return new DTOEnrollGetInfo(Id, Student!.Name, Course!.Name);
        }
    }
}