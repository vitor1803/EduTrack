using api.Dtos;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccessController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signinManager;
        private readonly TokenService tokenService;
        private readonly StudentRepository studentRepository;

        private readonly TeacherRepository teacherRepository;

        public AccessController
        (
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signinManager,
            TokenService tokenService,
            StudentRepository studentRepository,
            TeacherRepository teacherRepository
        )
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
            this.tokenService = tokenService;
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(DTOLogin loginDto)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Username not found and/or password incorrect");
            var result = await signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
            return Ok(new DTOUser(user.UserName!, user.Email!, await tokenService.CreateToken(user)));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                // Pega todos os usuários do Identity
                var users = await userManager.Users
                    .Select(u => new DTOUser(
                        u.UserName!,
                        u.Email!,
                        "" // opcional: gerar token
                    ))
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signup/student")]
        public async Task<IActionResult> SignUpStudent([FromBody] DTOSingUpStudent dto)
        {
            var (appUser, errors)  = await studentRepository.Create(dto);

            if (appUser == null)
                return StatusCode(500, errors);
            else
                return Ok(appUser.UserName);
        }

        [HttpPost("signup/teacher")]
        public async Task<IActionResult> SignUpTeacher([FromBody] DTOSingUpTeacher dto)
        {
            var (appUser, errors)  = await teacherRepository.Create(dto);

            if (appUser == null)
                return StatusCode(500, errors);
            else
                return Ok(appUser.UserName);
        }
    }
}