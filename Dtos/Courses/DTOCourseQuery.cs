using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class DTOCourseQuery
    {
        public string? Name { get; set; } = null;
        public string? TeacherName { get; set; } = null;
        public CourseSortBy? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }

        public enum CourseSortBy { Name }
}
