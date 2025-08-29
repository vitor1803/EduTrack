using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOCourseCreate(
        [Required] string Name,
        [Required] int TeacherId
    );
}
