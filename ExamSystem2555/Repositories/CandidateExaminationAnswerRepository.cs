using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class CandidateExaminationAnswerRepository : IAsyncGenericRepository<CandidateExaminationAnswer>
    {
        private readonly ApplicationDbContext _context;
        public CandidateExaminationAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExaminationAnswer> AddAsync(CandidateExaminationAnswer examCandidateAnswer)
        {
            await _context.CandidateExaminationAnswers.AddAsync(examCandidateAnswer);
            return examCandidateAnswer;
        }

        public async Task<CandidateExaminationAnswer> UpdateAsync(CandidateExaminationAnswer examCandidateAnswer)
        {
            _context.CandidateExaminationAnswers.Attach(examCandidateAnswer);
            _context.Entry(examCandidateAnswer).State = EntityState.Modified;
            return examCandidateAnswer;
        }

        public async Task DeleteAsync(int? id)
        {
            var examCandidateAnswerToDelete = await GetByIdAsync(id);
            _context.CandidateExaminationAnswers.Remove(examCandidateAnswerToDelete);
        }
        public async Task<CandidateExaminationAnswer> GetByIdAsync(int? id) => await _context.CandidateExaminationAnswers.FindAsync(id);
        public async Task<IEnumerable<CandidateExaminationAnswer>> GetAllAsync() => await _context.CandidateExaminationAnswers.ToListAsync();

        public Task<IEnumerable<CandidateExaminationAnswer>> AddRangeAsync(IEnumerable<CandidateExaminationAnswer> entities)
        {
            throw new NotImplementedException();
        }
    }
}
