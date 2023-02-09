using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class CandidateExaminationResultsRepository: IAsyncGenericRepository<CandidateExaminationResult>
    {

        private readonly ApplicationDbContext _context;

        public CandidateExaminationResultsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExaminationResult> AddAsync(CandidateExaminationResult candidateExamResult)
        {
            await _context.Set<CandidateExaminationResult>().AddAsync(candidateExamResult);
            return candidateExamResult;
        }

        public async Task<CandidateExaminationResult> UpdateAsync(CandidateExaminationResult candidateExamResult)
        {
            _context.CandidateExamResults.Attach(candidateExamResult);
            _context.Entry(candidateExamResult).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return candidateExamResult;
        }

        public async Task DeleteAsync(int? id)
        {
            var candidateExamResultToDelete = await GetByIdAsync(id);
            _context.CandidateExamResults.Remove(candidateExamResultToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<CandidateExaminationResult> GetByIdAsync(int? id)
        {
            return await _context.CandidateExamResults.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CandidateExaminationResult>> GetAllAsync()
        {

            return await _context.CandidateExamResults.ToListAsync();
        }

        public Task<IEnumerable<CandidateExaminationResult>> AddRangeAsync(IEnumerable<CandidateExaminationResult> entities)
        {
            throw new NotImplementedException();
        }
    }
}
