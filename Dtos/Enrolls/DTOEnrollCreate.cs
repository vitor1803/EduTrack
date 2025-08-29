using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOEnrollCreate(
        [Required] int StudentId,
        [Required] int CourseId 
    );
}