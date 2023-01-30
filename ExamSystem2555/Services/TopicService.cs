using WebApp.Repositories;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class TopicService : ITopicService
    {
        private IAsyncGenericRepository<Topic> _topicRepository;

        public TopicService(IAsyncGenericRepository<Topic> topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<Topic> GetTopicByIdAsync(int? id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await _topicRepository.GetAllAsync();
        }

        public async Task<Topic> AddTopicAsync(Topic topic)
        {
            return await _topicRepository.AddAsync(topic);
        }

        public async Task<Topic> UpdateTopicAsync(Topic topic)
        {
            return await (_topicRepository.UpdateAsync(topic));
        }

        public async Task DeleteTopicAsync(int? id)
        {
            await _topicRepository.DeleteAsync(id);
        }

        public string CheckNull(Topic topic)
        {
            if (topic != null)
            {
                return "OK";
            }

            return null;
        }

        public async Task<IEnumerable<Topic>> SortTopicsById(IEnumerable<int> topicIds)
        {
            var sortedTopics = (await GetAllTopicsAsync()).Where(ci => topicIds.Contains(ci.TopicId)).ToList();
            return sortedTopics;
        }
    }
}
