using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{

    public class AddressRepository : IAsyncGenericRepository<CandidateAddress>
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateAddress> AddAsync(CandidateAddress address)
        {
            await _context.Addresses.AddAsync(address);
            return address;
        }

        public async Task<CandidateAddress> UpdateAsync(CandidateAddress address)
        {
            _context.Addresses.Attach(address);
            _context.Entry(address).State = EntityState.Modified;
            return address;
        }

        public async Task DeleteAsync(int? id)
        {
            var addressToDelete = await GetByIdAsync(id);
            _context.Addresses.Remove(addressToDelete);
        }
        public async Task<CandidateAddress> GetByIdAsync(int? id) => await _context.Addresses.FindAsync(id);
        public async Task<IEnumerable<CandidateAddress>> GetAllAsync() => await _context.Addresses.ToListAsync();

        public Task<IEnumerable<CandidateAddress>> AddRangeAsync(IEnumerable<CandidateAddress> entities)
        {
            throw new NotImplementedException();
        }
    }

}
