using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class CertificateLevelRepository : IAsyncGenericRepository<CertificateLevel>
    {
        private readonly ApplicationDbContext _context;

        public CertificateLevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CertificateLevel> AddAsync(CertificateLevel level)
        {
            await _context.Set<CertificateLevel>().AddAsync(level);
            return level;
        }

        public async Task<CertificateLevel> UpdateAsync(CertificateLevel level)
        {
            _context.Levels.Attach(level);
            _context.Entry(level).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return level;
        }

        public async Task DeleteAsync(int? id)
        {
            var levelToDelete = await GetByIdAsync(id);
            _context.Levels.Remove(levelToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<CertificateLevel> GetByIdAsync(int? id)
        {
            return await _context.Levels.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CertificateLevel>> GetAllAsync()
        {
            
            return await _context.Levels.ToListAsync();
        }

        public Task<IEnumerable<CertificateLevel>> AddRangeAsync(IEnumerable<CertificateLevel> entities)
        {
            throw new NotImplementedException();
        }
    }
}

