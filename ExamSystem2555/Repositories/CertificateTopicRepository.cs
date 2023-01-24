using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;

namespace WebApp.Repositories
{
    public class CertificateTopicRepository:IAsyncGenericRepository<CertificateTopic>
    {
        private readonly ApplicationDbContext _context;

        public CertificateTopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CertificateTopic> AddAsync(CertificateTopic cTQuestion)
        {
            await _context.Set<CertificateTopic>().AddAsync(cTQuestion);
            return cTQuestion;
        }

        public async Task<CertificateTopic> UpdateAsync(CertificateTopic cTQuestion)
        {
            _context.CertificateTopics.Attach(cTQuestion);
            _context.Entry(cTQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cTQuestion;
        }

        public async Task DeleteAsync(int? id)
        {
            var cTQuestion = await GetByIdAsync(id);
            _context.CertificateTopics.Remove(cTQuestion);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<CertificateTopic> GetByIdAsync(int? id)
        {
            return await _context.CertificateTopics.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CertificateTopic>> GetAllAsync()
        {

            return await _context.CertificateTopics.ToListAsync();
        }

        public Task<IEnumerable<CertificateTopic>> AddRangeAsync(IEnumerable<CertificateTopic> entities)
        {
            throw new NotImplementedException();
        }
    }
}
