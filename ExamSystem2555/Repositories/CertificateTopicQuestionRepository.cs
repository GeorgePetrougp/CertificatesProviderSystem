using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class CertificateTopicQuestionRepository : IAsyncGenericRepository<CertificateTopicQuestion>
    {
        private readonly ApplicationDbContext _context;

        public CertificateTopicQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CertificateTopicQuestion> AddAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            await _context.Set<CertificateTopicQuestion>().AddAsync(certificateTopicQuestion);
            return certificateTopicQuestion;
        }

        public async Task<CertificateTopicQuestion> UpdateAsync(CertificateTopicQuestion certificateTopicQuestion)
        {
            _context.CertificateTopicQuestions.Attach(certificateTopicQuestion);
            _context.Entry(certificateTopicQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return certificateTopicQuestion;
        }

        public async Task DeleteAsync(int? id)
        {
            var certificateTopicQuestion = await GetByIdAsync(id);
            _context.CertificateTopicQuestions.Remove(certificateTopicQuestion);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<CertificateTopicQuestion> GetByIdAsync(int? id)
        {
            return await _context.CertificateTopicQuestions.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CertificateTopicQuestion>> GetAllAsync()
        {

            return await _context.CertificateTopicQuestions.ToListAsync();
        }

        public async Task<IEnumerable<CertificateTopicQuestion>> AddRangeAsync(IEnumerable<CertificateTopicQuestion> entities)
        {
            await _context.CertificateTopicQuestions.AddRangeAsync(entities);
            return entities;
        }
    }
}
