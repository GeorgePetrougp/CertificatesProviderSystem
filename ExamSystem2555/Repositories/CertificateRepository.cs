using MyDatabase.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repositories
{
    public class CertificateRepository : IAsyncGenericRepository<Certificate>
    {
        private readonly ApplicationDbContext _context;
        public CertificateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            await _context.Certificates.AddAsync(certificate);
            return certificate;
        }

        public async Task<Certificate> UpdateAsync(Certificate certificate)
        {
            _context.Certificates.Attach(certificate);
            _context.Entry(certificate).State = EntityState.Modified;
            return certificate;
        }

        public async Task DeleteAsync(int? id)
        {
            var certificateToDelete = await GetByIdAsync(id);
            _context.Certificates.Remove(certificateToDelete);
        }
        public async Task<Certificate> GetByIdAsync(int? id) => await _context.Certificates.FindAsync(id);
        public async Task<IEnumerable<Certificate>> GetAllAsync() => await _context.Certificates.ToListAsync();

        
    }
}
