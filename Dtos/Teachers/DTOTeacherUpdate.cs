using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOTeacherUpdate(
        [MinLength(3)] string? Name,
        [MinLength(3)] string? Login,
        [MinLength(3)] string? Password
    );
}