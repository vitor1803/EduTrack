namespace api.Dtos
{
    public record DTOTeacherGetInfo(
        int Id,
        string Name,
        List<DTOCourseGetInfo> Courses
    );
}