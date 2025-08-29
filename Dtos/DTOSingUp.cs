using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOSingUp(
        [Required][MinLength(3)] string Name,
        [Required][EmailAddress] string EMail,
        [Required] string Username,
        [Required] string Password
    );
}
