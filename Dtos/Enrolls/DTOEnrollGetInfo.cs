using api.Models;

namespace api.Dtos
{
    public record DTOEnrollGetInfo(
        int Id,
        string Course,
        string Students
    );
}
