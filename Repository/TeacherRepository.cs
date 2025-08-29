using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TeacherRepository
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<AppUser> userManager;

        public TeacherRepository(ApplicationDBContext dBContext, UserManager<AppUser> userManager)
        {
            context = dBContext;
            this.userManager = userManager;
        }

        public async Task<List<DTOTeacherGetInfo>> GetAllAsync(DTOTeacherQuery query)
        {
            var teachers = context.Teachers.Include(c => c.Courses).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                teachers = teachers.Where(t => t.Name.Contains(query.Name));

            if (query.SortBy.HasValue)
            {
                if (query.SortBy.Value == TeacherSortBy.Name)
                    teachers = query.IsDescending ? teachers.OrderByDescending(s => s.Name) : teachers.OrderBy(s => s.Name);
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await teachers.Skip(skipNumber).Take(query.PageSize).Select(s => s.ToDTO()).ToListAsync();
        }

        public async Task<DTOTeacherGetInfo?> GetByIdAsync(int id)
        {
            var teacher = await context.Teachers.FindAsync(id);
            return teacher?.ToDTO();
        }

        public async Task<DTOTeacherGetInfo> CreateTeacher(DTOTeacherCreate dtoTeacherCreate)
        {
            var teacher = new Teacher(dtoTeacherCreate);
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();
            return teacher.ToDTO();
        }

        public async Task<(AppUser? user, string[]? errors)> Create(DTOSingUpTeacher dto)
        {
            var appUser = new AppUser
            {
                UserName = dto.Username,
                Email = dto.EMail
            };
            var createdUser = await userManager.CreateAsync(appUser, dto.Password);
            if (!createdUser.Succeeded)
                return (null, createdUser.Errors.Select(e => e.Description).ToArray());

            var roleAdded = await userManager.AddToRoleAsync(appUser, "Teacher");
            if (!roleAdded.Succeeded)
                return (null, roleAdded.Errors.Select(e => e.Description).ToArray());

            var teacher = new Teacher(dto.Name, appUser.Id);
            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();
            return (appUser, null);
        }

        public async Task<DTOTeacherGetInfo?> UpdateTeacher(int id, DTOTeacherUpdate dtoTeacherUpdate)
        {
            var teacher = await context.Teachers.FindAsync(id);
            if (teacher == null) return null;

            teacher.Name = dtoTeacherUpdate.Name ?? teacher.Name;

            await context.SaveChangesAsync();
            return teacher.ToDTO();
        }

        public async Task<DTOTeacherGetInfo?> DeleteTeacher( int id)
        {
            var teacher = await context.Teachers.FindAsync(id);
            if (teacher == null) return null;

            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();
            return teacher.ToDTO();
        }
    }
}