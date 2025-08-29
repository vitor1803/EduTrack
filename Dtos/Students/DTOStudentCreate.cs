using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOStudentCreate(
        [Required][MinLength(3)] string Name
    );
}