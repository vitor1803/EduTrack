using api.Data;
using api.Dtos;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly StudentRepository studentRepository;


        public StudentsController(ApplicationDBContext dBContext, StudentRepository studentRepository)
        {
            context = dBContext;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DTOStudentQuery query)
        {
            var students = await studentRepository.GetAllAsync(query);
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await studentRepository.GetByIdAsync(id);
            return student is not null ? Ok(student) : NotFound();
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DTOTeacherUpdate dto)
        {
            var student = await studentRepository.Update(id, dto);
            return student is not null ? Ok(student) : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            var student = await studentRepository.Delete(id);
            return student is not null ? NoContent() : NotFound();
        }
    }
}