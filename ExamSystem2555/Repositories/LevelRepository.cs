using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class LevelRepository : IAsyncGenericRepository<Level>
    {
        private readonly ApplicationDbContext _context;

        public LevelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Level> AddAsync(Level level)
        {
            await _context.Set<Level>().AddAsync(level);
            return level;
        }

        public async Task<Level> UpdateAsync(Level level)
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
        public async Task<Level> GetByIdAsync(int? id)
        {
            return await _context.Levels.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            
            return await _context.Levels.ToListAsync();
        }

        public Task<IEnumerable<Level>> AddRangeAsync(IEnumerable<Level> entities)
        {
            throw new NotImplementedException();
        }
    }
}

