using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{

    public class CandidateAddressRepository : IAsyncGenericRepository<CandidateAddress>
    {
        private readonly ApplicationDbContext _context;
        public CandidateAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateAddress> AddAsync(CandidateAddress address)
        {
            await _context.CandidateAddresses.AddAsync(address);
            return address;
        }

        public async Task<CandidateAddress> UpdateAsync(CandidateAddress address)
        {
            _context.CandidateAddresses.Attach(address);
            _context.Entry(address).State = EntityState.Modified;
            return address;
        }

        public async Task DeleteAsync(int? id)
        {
            var addressToDelete = await GetByIdAsync(id);
            _context.CandidateAddresses.Remove(addressToDelete);
        }
        public async Task<CandidateAddress> GetByIdAsync(int? id) => await _context.CandidateAddresses.FindAsync(id);
        public async Task<IEnumerable<CandidateAddress>> GetAllAsync() => await _context.CandidateAddresses.ToListAsync();

        public Task<IEnumerable<CandidateAddress>> AddRangeAsync(IEnumerable<CandidateAddress> entities)
        {
            throw new NotImplementedException();
        }
    }

}
