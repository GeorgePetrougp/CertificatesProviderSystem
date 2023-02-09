using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class QuestionDifficultyRepository : IAsyncGenericRepository<QuestionDifficulty>
    {
        private readonly ApplicationDbContext _context;

        public QuestionDifficultyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuestionDifficulty> AddAsync(QuestionDifficulty difficulty)
        {
            await _context.QuestionDifficulties.AddAsync(difficulty);
            return difficulty;
        }

        public async Task<QuestionDifficulty> UpdateAsync(QuestionDifficulty difficulty)
        {
            _context.QuestionDifficulties.Attach(difficulty);
            _context.Entry(difficulty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return difficulty;
        }

        public async Task DeleteAsync(int? id)
        {
            var difficultyToDelete = await GetByIdAsync(id);
            _context.QuestionDifficulties.Remove(difficultyToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<QuestionDifficulty> GetByIdAsync(int? id)
        {

            return await _context.QuestionDifficulties.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<QuestionDifficulty>> GetAllAsync()
        {
            return await _context.QuestionDifficulties.ToListAsync();
        }

        public Task<IEnumerable<QuestionDifficulty>> AddRangeAsync(IEnumerable<QuestionDifficulty> entities)
        {
            throw new NotImplementedException();
        }
    }
}
