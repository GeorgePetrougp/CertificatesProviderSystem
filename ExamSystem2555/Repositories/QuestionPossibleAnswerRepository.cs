using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class QuestionPossibleAnswerRepository:IAsyncGenericRepository<QuestionPossibleAnswer>
    {
        private readonly ApplicationDbContext _context;
        public QuestionPossibleAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuestionPossibleAnswer> AddAsync(QuestionPossibleAnswer answer)
        {
            await _context.QuestionPossibleAnswers.AddAsync(answer);
            return answer;
        }

        public async Task<IEnumerable<QuestionPossibleAnswer>> AddRangeAsync(IEnumerable<QuestionPossibleAnswer> answer)
        {
            await _context.QuestionPossibleAnswers.AddRangeAsync(answer);
            return answer;
        }

        public async Task<QuestionPossibleAnswer> UpdateAsync(QuestionPossibleAnswer answer)
        {
            _context.QuestionPossibleAnswers.Attach(answer);
            _context.Entry(answer).State = EntityState.Modified;
            return answer;
        }

        public async Task DeleteAsync(int? id)
        {
            var answerToDelete = await GetByIdAsync(id);
            _context.QuestionPossibleAnswers.Remove(answerToDelete);
        }
        public async Task<QuestionPossibleAnswer> GetByIdAsync(int? id) => await _context.QuestionPossibleAnswers.FindAsync(id);
        public async Task<IEnumerable<QuestionPossibleAnswer>> GetAllAsync() => await _context.QuestionPossibleAnswers.ToListAsync();

    }
}
