using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class ExamCandidateAnswerRepository : IAsyncGenericRepository<ExamCandidateAnswer>
    {
        private readonly ApplicationDbContext _context;
        public ExamCandidateAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExamCandidateAnswer> AddAsync(ExamCandidateAnswer examCandidateAnswer)
        {
            await _context.ExamCandidateAnswers.AddAsync(examCandidateAnswer);
            return examCandidateAnswer;
        }

        public async Task<ExamCandidateAnswer> UpdateAsync(ExamCandidateAnswer examCandidateAnswer)
        {
            _context.ExamCandidateAnswers.Attach(examCandidateAnswer);
            _context.Entry(examCandidateAnswer).State = EntityState.Modified;
            return examCandidateAnswer;
        }

        public async Task DeleteAsync(int? id)
        {
            var examCandidateAnswerToDelete = await GetByIdAsync(id);
            _context.ExamCandidateAnswers.Remove(examCandidateAnswerToDelete);
        }
        public async Task<ExamCandidateAnswer> GetByIdAsync(int? id) => await _context.ExamCandidateAnswers.FindAsync(id);
        public async Task<IEnumerable<ExamCandidateAnswer>> GetAllAsync() => await _context.ExamCandidateAnswers.ToListAsync();

        public Task<IEnumerable<ExamCandidateAnswer>> AddRangeAsync(IEnumerable<ExamCandidateAnswer> entities)
        {
            throw new NotImplementedException();
        }
    }
}
