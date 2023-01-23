using MyDatabase.Data;
using MyDatabase.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repositories
{
    public class QuestionRepository : IAsyncGenericRepository<Question>
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Question> AddAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            return question;
        }

        public async Task<Question> UpdateAsync(Question question)
        {
            _context.Questions.Attach(question);
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task DeleteAsync(int? id)
        {
            var questionToDelete = await GetByIdAsync(id);
            _context.Questions.Remove(questionToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<Question> GetByIdAsync(int? id)
        {
            return await _context.Questions.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions.ToListAsync();
        }

       


    }
}