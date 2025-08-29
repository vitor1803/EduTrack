using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EnrollRepository
    {
        private readonly ApplicationDBContext context;

        public EnrollRepository(ApplicationDBContext dbContext)
        {
            context = dbContext;
        }
        public async Task<List<DTOEnrollGetInfo>> GetAllAsync(DTOEnrollQuery query)
        {
            var enrolls = context.Enrolls.Include(e => e.Student).Include(e => e.Course).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CourseName))
                enrolls = enrolls.Where(e => e.Course!.Name.Contains(query.CourseName));
            if (!string.IsNullOrWhiteSpace(query.StudentName))
                enrolls = enrolls.Where(e => e.Student!.Name.Contains(query.StudentName));

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await enrolls.Skip(skipNumber).Take(query.PageSize).Select(e => e.ToDTO()).ToListAsync();
        }

        public async Task<Enroll> CreateAsync(Enroll enroll)
        {
            context.Enrolls.Add(enroll);
            await context.SaveChangesAsync();
            return enroll;
        }

        public async Task<Enroll?> GetByIdAsync(int id)
        {
            return await context.Enrolls
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(Enroll enroll)
        {
            context.Enrolls.Update(enroll);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enroll = await context.Enrolls.FindAsync(id);
            if (enroll != null)
            {
                context.Enrolls.Remove(enroll);
                await context.SaveChangesAsync();
            }
        }
    }

}