using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StudentRepository
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<AppUser> userManager;

        public StudentRepository(ApplicationDBContext dBContext, UserManager<AppUser> userManager)
        {
            context = dBContext;
            this.userManager = userManager;
        }

    
        public async Task<List<DTOStudentGetInfo>> GetAllAsync(DTOStudentQuery query)
        {
            var students = context.Students.Include(s => s.Enrolls).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                students = students.Where(s => s.Name.Contains(query.Name));

            if (query.SortBy.HasValue)
            {
                if (query.SortBy.Value == TeacherSortBy.Name)
                    students = query.IsDescending ? students.OrderByDescending(s => s.Name) : students.OrderBy(s => s.Name);
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await students.Skip(skipNumber).Take(query.PageSize).Select(s => s.ToDTO()).ToListAsync();
        }

        public async Task<DTOStudentGetInfo?> GetByIdAsync(int id)
        {
            var students = await context.Students.FindAsync(id);
            return students?.ToDTO();
        }

        public async Task<(AppUser? user, string[]? errors)>  Create(DTOSingUpStudent dto)
        {
            var appUser = new AppUser
            {
                UserName = dto.Username,
                Email = dto.EMail
            };
            var createdUser = await userManager.CreateAsync(appUser, dto.Password);
            if (!createdUser.Succeeded)
                return (null, createdUser.Errors.Select(e => e.Description).ToArray());

            var roleAdded = await userManager.AddToRoleAsync(appUser, "Student");
            if (!roleAdded.Succeeded)
                return (null, roleAdded.Errors.Select(e => e.Description).ToArray());

            var student = new Student(dto.Name, appUser.Id);
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return (appUser, null);
        }

        public async Task<DTOStudentGetInfo?> Update(int id, DTOTeacherUpdate dtoTeacherUpdate)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null) return null;

            student.Name = dtoTeacherUpdate.Name ?? student.Name;

            await context.SaveChangesAsync();
            return student.ToDTO();
        }

        public async Task<DTOStudentGetInfo?> Delete(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null) return null;

            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return student.ToDTO();
        }
    }
}