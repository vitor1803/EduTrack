using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class DTOEnrollQuery
    {
        public string? CourseName { get; set; } = null;
        public string? StudentName { get; set; } = null;
        public CourseSortBy? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}
