using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class ExamCandidateAnswerRepository : IAsyncGenericRepository<CandidateExaminationAnswer>
    {
        private readonly ApplicationDbContext _context;
        public ExamCandidateAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExaminationAnswer> AddAsync(CandidateExaminationAnswer examCandidateAnswer)
        {
            await _context.ExamCandidateAnswers.AddAsync(examCandidateAnswer);
            return examCandidateAnswer;
        }

        public async Task<CandidateExaminationAnswer> UpdateAsync(CandidateExaminationAnswer examCandidateAnswer)
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
        public async Task<CandidateExaminationAnswer> GetByIdAsync(int? id) => await _context.ExamCandidateAnswers.FindAsync(id);
        public async Task<IEnumerable<CandidateExaminationAnswer>> GetAllAsync() => await _context.ExamCandidateAnswers.ToListAsync();

        public Task<IEnumerable<CandidateExaminationAnswer>> AddRangeAsync(IEnumerable<CandidateExaminationAnswer> entities)
        {
            throw new NotImplementedException();
        }
    }
}
