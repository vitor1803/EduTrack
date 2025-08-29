using api.Dtos;

namespace api.Dtos
{
    public class DTOStudentQuery{

        public string? Name { get; set; } = null;
        public TeacherSortBy? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }

    public enum StudentSortBy { Name }
}
