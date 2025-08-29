using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOTeacherCreate(
        [Required][MinLength(3)] string Name
    );
}