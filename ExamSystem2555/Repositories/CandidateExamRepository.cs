using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class CandidateExamRepository : IAsyncGenericRepository<CandidateExamination>
    {
        private readonly ApplicationDbContext _context;

        public CandidateExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateExamination> AddAsync(CandidateExamination candidateExam)
        {
            await _context.Set<CandidateExamination>().AddAsync(candidateExam);
            return candidateExam;
        }

        public async Task<CandidateExamination> UpdateAsync(CandidateExamination candidateExam)
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
        public async Task<CandidateExamination> GetByIdAsync(int? id)
        {
            return await _context.CandidateExams.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<CandidateExamination>> GetAllAsync()
        {

            return await _context.CandidateExams.ToListAsync();
        }

        public Task<IEnumerable<CandidateExamination>> AddRangeAsync(IEnumerable<CandidateExamination> entities)
        {
            throw new NotImplementedException();
        }
    }
}
