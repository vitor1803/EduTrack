using api.Dtos;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    { 
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
    }
}