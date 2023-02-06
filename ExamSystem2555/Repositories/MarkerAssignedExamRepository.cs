using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class MarkerAssignedExamRepository : IAsyncGenericRepository<MarkerAssignedExam>
    {
        private readonly ApplicationDbContext _context;

        public MarkerAssignedExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MarkerAssignedExam> AddAsync(MarkerAssignedExam markerAssignedExam)
        {
            await _context.Set<MarkerAssignedExam>().AddAsync(markerAssignedExam);
            return markerAssignedExam;
        }

        public async Task<MarkerAssignedExam> UpdateAsync(MarkerAssignedExam markerAssignedExam)
        {
            _context.MarkerAssignedExams.Attach(markerAssignedExam);
            _context.Entry(markerAssignedExam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return markerAssignedExam;
        }

        public async Task DeleteAsync(int? id)
        {
            var markerAssignedExamDelete = await GetByIdAsync(id);
            _context.MarkerAssignedExams.Remove(markerAssignedExamDelete);
        }
        public async Task<MarkerAssignedExam> GetByIdAsync(int? id)
        {
            return await _context.MarkerAssignedExams.FindAsync(id);

        }

        public async Task<IEnumerable<MarkerAssignedExam>> GetAllAsync()
        {

            return await _context.MarkerAssignedExams.ToListAsync();
        }

        public Task<IEnumerable<MarkerAssignedExam>> AddRangeAsync(IEnumerable<MarkerAssignedExam> entities)
        {
            throw new NotImplementedException();
        }
    }
}
