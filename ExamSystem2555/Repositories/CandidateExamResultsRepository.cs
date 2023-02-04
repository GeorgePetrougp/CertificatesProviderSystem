using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;

namespace WebApp.Repositories
{
    public class CandidateExamResultsRepository: IAsyncGenericRepository<CandidateExamResults>
    {

        private readonly ApplicationDbContext _context;

        public CandidateExamResultsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExamResults> AddAsync(CandidateExamResults candidateExamResult)
        {
            await _context.Set<CandidateExamResults>().AddAsync(candidateExamResult);
            return candidateExamResult;
        }

        public async Task<CandidateExamResults> UpdateAsync(CandidateExamResults candidateExamResult)
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
        public async Task<CandidateExamResults> GetByIdAsync(int? id)
        {
            return await _context.CandidateExamResults.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CandidateExamResults>> GetAllAsync()
        {

            return await _context.CandidateExamResults.ToListAsync();
        }

        public Task<IEnumerable<CandidateExamResults>> AddRangeAsync(IEnumerable<CandidateExamResults> entities)
        {
            throw new NotImplementedException();
        }
    }
}
