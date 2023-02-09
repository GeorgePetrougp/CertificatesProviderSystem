using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Repositories.Interfaces;

namespace WebApp.Repositories
{
    public class ExaminationQuestionRepository : IAsyncGenericRepository<ExaminationQuestion>
    {
        private readonly ApplicationDbContext _context;

        public ExaminationQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExaminationQuestion> AddAsync(ExaminationQuestion examinationQuestion)
        {
            await _context.Set<ExaminationQuestion>().AddAsync(examinationQuestion);
            return examinationQuestion;
        }

        public async Task<ExaminationQuestion> UpdateAsync(ExaminationQuestion examinationQuestion)
        {
            _context.ExamQuestions.Attach(examinationQuestion);
            _context.Entry(examinationQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return examinationQuestion;
        }

        public async Task DeleteAsync(int? id)
        {
            var examinationQuestionToDelete = await GetByIdAsync(id);
            _context.ExamQuestions.Remove(examinationQuestionToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<ExaminationQuestion> GetByIdAsync(int? id)
        {

            return await _context.ExamQuestions.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<ExaminationQuestion>> GetAllAsync()
        {
            return await _context.ExamQuestions.ToListAsync();
        }

        public Task<IEnumerable<ExaminationQuestion>> AddRangeAsync(IEnumerable<ExaminationQuestion> entities)
        {
            throw new NotImplementedException();
        }
    }
}
