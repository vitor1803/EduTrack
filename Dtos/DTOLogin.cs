using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOLogin(
        [Required] string Username,
        [Required] string Password
    );
}
