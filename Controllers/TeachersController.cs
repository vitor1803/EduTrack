using api.Data;
using api.Dtos;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly TeacherRepository teacherRepository;


        public TeachersController(ApplicationDBContext dBContext, TeacherRepository teacherRepository)
        {
            context = dBContext;
            this.teacherRepository = teacherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DTOTeacherQuery query)
        {
            var teachers = await teacherRepository.GetAllAsync(query);
            return Ok(teachers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var teacher = await teacherRepository.GetByIdAsync(id);
            return teacher is not null ? Ok(teacher) : NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTeacher([FromRoute] int id, [FromBody] DTOTeacherUpdate dtoTeacherUpdate)
        {
            var teacher = await teacherRepository.UpdateTeacher(id, dtoTeacherUpdate);
            return teacher is not null ? Ok(teacher) : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            var teacher = await teacherRepository.DeleteTeacher(id);
            return teacher is not null ? NoContent() : NotFound();
        }
    }
}