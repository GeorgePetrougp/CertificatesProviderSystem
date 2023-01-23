using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{

    public class AddressRepository : IAsyncGenericRepository<Address>
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address> AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            return address;
        }

        public async Task<Address> UpdateAsync(Address address)
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
        public async Task<Address> GetByIdAsync(int? id) => await _context.Addresses.FindAsync(id);
        public async Task<IEnumerable<Address>> GetAllAsync() => await _context.Addresses.ToListAsync();


    }

}
