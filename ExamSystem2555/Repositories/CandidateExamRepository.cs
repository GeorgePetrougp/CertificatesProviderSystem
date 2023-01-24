using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class CandidateExamRepository : IAsyncGenericRepository<CandidateExam>
    {
        private readonly ApplicationDbContext _context;

        public CandidateExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExam> AddAsync(CandidateExam candidateExam)
        {
            await _context.Set<CandidateExam>().AddAsync(candidateExam);
            return candidateExam;
        }

        public async Task<CandidateExam> UpdateAsync(CandidateExam candidateExam)
        {
            _context.CandidateExams.Attach(candidateExam);
            _context.Entry(candidateExam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return candidateExam;
        }

        public async Task DeleteAsync(int? id)
        {
            var candidateExamToDelete = await GetByIdAsync(id);
            _context.CandidateExams.Remove(candidateExamToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<CandidateExam> GetByIdAsync(int? id)
        {
            return await _context.CandidateExams.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CandidateExam>> GetAllAsync()
        {

            return await _context.CandidateExams.ToListAsync();
        }

        public Task<IEnumerable<CandidateExam>> AddRangeAsync(IEnumerable<CandidateExam> entities)
        {
            throw new NotImplementedException();
        }
    }
}
