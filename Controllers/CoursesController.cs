using api.Data;
using api.Dtos;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private CourseRepository CRepo;


        public CoursesController(ApplicationDBContext dBContext, CourseRepository courseRepository)
        {
            context = dBContext;
            CRepo = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DTOCourseQuery query)
        {
            var courses = await CRepo.GetAllAsync(query);
            return Ok(courses);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var teacher = await CRepo.GetByIdAsync(id);
            return teacher is not null ? Ok(teacher) : NotFound();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] DTOCourseCreate dto)
        {
            var dtoCourse = await CRepo.CreateCourse(dto);
            return dtoCourse is not null ? CreatedAtAction(nameof(GetById), new { id = dtoCourse.Id }, dtoCourse) : NotFound("Unknown teacher");
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int id, [FromBody] DTOCourseUpdate dto)
        {
            var dtoCourse = await CRepo.UpdateCourse(id, dto);
            return dtoCourse is not null ? Ok(dtoCourse) : NotFound();
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var teacher = await CRepo.DeleteCourse(id);
            return teacher is not null ? NoContent() : NotFound();
        }
    }
}