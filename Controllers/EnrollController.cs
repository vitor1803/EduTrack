using api.Data;
using api.Dtos;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/enroll")]
    public class EnrollsController : ControllerBase
    {
        private readonly EnrollRepository enrollRepo;
        private readonly StudentRepository studentRepo;
        private readonly CourseRepository courseRepo;

        public EnrollsController(
            EnrollRepository enrollRepo,
            StudentRepository studentRepo,
            CourseRepository courseRepo)
        {
            this.enrollRepo = enrollRepo;
            this.studentRepo = studentRepo;
            this.courseRepo = courseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<DTOEnrollGetInfo>>> GetAll([FromQuery] DTOEnrollQuery query)
        {
            var result = await enrollRepo.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTOEnrollGetInfo>> GetById(int id)
        {
            var enroll = await enrollRepo.GetByIdAsync(id);
            if (enroll == null) return NotFound();
            return Ok(enroll.ToDTO());
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<ActionResult<DTOEnrollGetInfo>> Create(DTOEnrollCreate dto)
        {
            // garantir que Student e Course existem
            var student = await studentRepo.GetByIdAsync(dto.StudentId);
            if (student == null) return BadRequest($"Student {dto.StudentId} not found");

            var course = await courseRepo.GetByIdAsync(dto.CourseId);
            if (course == null) return BadRequest($"Course {dto.CourseId} not found");

            // criar enroll
            var enroll = new Enroll(dto);

            await enrollRepo.CreateAsync(enroll);

            return CreatedAtAction(nameof(GetById), new { id = enroll.Id }, enroll.ToDTO());
        }
    }
}