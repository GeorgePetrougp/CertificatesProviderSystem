using MyDatabase.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repositories
{
    public class CandidateRepository : IAsyncGenericRepository<Candidate>
    {
        private readonly ApplicationDbContext _context;
        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            return candidate;
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Attach(candidate);
            _context.Entry(candidate).State = EntityState.Modified;
            return candidate;
        }

        public async Task DeleteAsync(int? id)
        {
            var candidateToDelete = await GetByIdAsync(id);
            _context.Candidates.Remove(candidateToDelete);
        }
        public async Task<Candidate> GetByIdAsync(int? id) => await _context.Candidates.FindAsync(id);
        public async Task<IEnumerable<Candidate>> GetAllAsync() => await _context.Candidates.ToListAsync();

        public Task<IEnumerable<Candidate>> AddRangeAsync(IEnumerable<Candidate> entities)
        {
            throw new NotImplementedException();
        }
    }
}
