using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Repositories
{
    public class TopicRepository : IAsyncGenericRepository<Topic>
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> AddAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
            return topic;
        }

        public async Task<Topic> UpdateAsync(Topic topic)
        {
            _context.Topics.Attach(topic);
            _context.Entry(topic).State = EntityState.Modified;
            return topic;
        }

        public async Task DeleteAsync(int? id)
        {
            var topicToDelete = await GetByIdAsync(id);
            _context.Topics.Remove(topicToDelete);
        }
        public async Task<Topic> GetByIdAsync(int? id) => await _context.Topics.FindAsync(id);
        public async Task<IEnumerable<Topic>> GetAllAsync() => await _context.Topics.ToListAsync();

        public Task<IEnumerable<Topic>> AddRangeAsync(IEnumerable<Topic> entities)
        {
            throw new NotImplementedException();
        }
    }
}
