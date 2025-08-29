using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CourseRepository
    {
        private readonly ApplicationDBContext context;

        public CourseRepository(ApplicationDBContext dBContext)
        {
            context = dBContext;
        }

        public async Task<List<DTOCourseGetInfo>> GetAllAsync(DTOCourseQuery query)
        {
            var courses = context.Courses.Include(c => c.Enrolls).ThenInclude(e => e.Student).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                courses = courses.Where(c => c.Name.Contains(query.Name));

            if (query.SortBy.HasValue)
            {
                if (query.SortBy.Value == CourseSortBy.Name)
                    courses = query.IsDescending ? courses.OrderByDescending(s => s.Name) : courses.OrderBy(s => s.Name);
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await courses.Skip(skipNumber).Take(query.PageSize).Select(s => s.ToDTOCourse()).ToListAsync();
        }

        public async Task<DTOCourseGetInfo?> GetByIdAsync(int id)
        {
            var courses = await context.Courses.FindAsync(id);
            return courses?.ToDTOCourse();
        }

        public async Task<DTOCourseGetInfo?> CreateCourse(DTOCourseCreate dto)
        {

            var teacher = await context.Teachers.FindAsync(dto.TeacherId);
            if (teacher == null) return null;

            var course = new Course(dto, teacher);
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
            return course.ToDTOCourse();
        }

        public async Task<DTOCourseGetInfo?> UpdateCourse(int id, DTOCourseUpdate dto)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null) return null;

            course.Name = dto.Name ?? course.Name;
            course.TeacherId = dto.TeacherId ?? course.TeacherId;
            await context.SaveChangesAsync();
            return course.ToDTOCourse();
        }

        public async Task<DTOCourseGetInfo?> DeleteCourse(int id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null) return null;

            context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return course.ToDTOCourse();
        }
    }
}