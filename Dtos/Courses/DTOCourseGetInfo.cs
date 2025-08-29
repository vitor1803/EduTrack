using api.Models;

namespace api.Dtos
{
    public record DTOCourseGetInfo(
        int Id,
        string Name ,
        List<DTOStudentGetInfo> Students
    );
}
