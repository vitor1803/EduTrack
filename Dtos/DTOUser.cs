using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public record DTOUser(
        [Required] string Username,
        [Required][EmailAddress] string EMail,
        [Required] string Token
    );
}
