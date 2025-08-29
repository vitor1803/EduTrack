using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOCourseUpdate(
        [MinLength(3)] string? Name,
        int? TeacherId
    );
}
