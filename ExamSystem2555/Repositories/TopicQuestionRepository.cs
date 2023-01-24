using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class TopicQuestionRepository : IAsyncGenericRepository<TopicQuestion>
    {
        private readonly ApplicationDbContext _context;

        public TopicQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TopicQuestion> AddAsync(TopicQuestion topicQuestion)
        {
            await _context.TopicQuestions.AddAsync(topicQuestion);
            return topicQuestion;
        }

        public async Task<TopicQuestion> UpdateAsync(TopicQuestion topicQuestion)
        {
            _context.TopicQuestions.Attach(topicQuestion);
            _context.Entry(topicQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return topicQuestion;
        }

        public async Task DeleteAsync(int? id)
        {
            var topicQuestionToDelete = await GetByIdAsync(id);
            _context.TopicQuestions.Remove(topicQuestionToDelete);
        }
        //public async Task<Question> GetByIdAsync(int? id) => await _context.Questions.FindAsync(id);
        public async Task<TopicQuestion> GetByIdAsync(int? id)
        {

            return await _context.TopicQuestions.FindAsync(id);

        }
        //public async Task<IEnumerable<Question>> GetAllAsync() =>await _context.Questions.ToListAsync();

        public async Task<IEnumerable<TopicQuestion>> GetAllAsync()
        {
            return await _context.TopicQuestions.ToListAsync();
        }

        public Task<IEnumerable<TopicQuestion>> AddRangeAsync(IEnumerable<TopicQuestion> entities)
        {
            throw new NotImplementedException();
        }
    }
}
