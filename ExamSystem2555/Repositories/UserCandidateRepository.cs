using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class UserCandidateRepository: IAsyncGenericRepository<UserCandidate>
    {
        private readonly ApplicationDbContext _context;
        public UserCandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserCandidate> AddAsync(UserCandidate userCandidate)
        {
            await _context.UserCandidates.AddAsync(userCandidate);
            return userCandidate;
        }

        public async Task<UserCandidate> UpdateAsync(UserCandidate userCandidate)
        {
            _context.UserCandidates.Attach(userCandidate);
            _context.Entry(userCandidate).State = EntityState.Modified;
            return userCandidate;
        }

        public async Task DeleteAsync(int? id)
        {
            var examToDelete = await GetByIdAsync(id);
            _context.UserCandidates.Remove(examToDelete);
        }
        public async Task<UserCandidate> GetByIdAsync(int? id) => await _context.UserCandidates.FindAsync(id);
        public async Task<IEnumerable<UserCandidate>> GetAllAsync() => await _context.UserCandidates.ToListAsync();

        public Task<IEnumerable<UserCandidate>> AddRangeAsync(IEnumerable<UserCandidate> entities)
        {
            throw new NotImplementedException();
        }
    }
}
