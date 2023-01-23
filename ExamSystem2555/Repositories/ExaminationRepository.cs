using MyDatabase.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repositories
{
    public class ExaminationRepository : IAsyncGenericRepository<Examination>
    {
        private readonly ApplicationDbContext _context;
        public ExaminationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Examination> AddAsync(Examination exam)
        {
            await _context.Examinations.AddAsync(exam);
            return exam;
        }

        public async Task<Examination> UpdateAsync(Examination exam)
        {
            _context.Examinations.Attach(exam);
            _context.Entry(exam).State = EntityState.Modified;
            return exam;
        }

        public async Task DeleteAsync(int? id)
        {
            var examToDelete = await GetByIdAsync(id);
            _context.Examinations.Remove(examToDelete);
        }
        public async Task<Examination> GetByIdAsync(int? id) => await _context.Examinations.FindAsync(id);
        public async Task<IEnumerable<Examination>> GetAllAsync() => await _context.Examinations.ToListAsync();

        
    }
}
