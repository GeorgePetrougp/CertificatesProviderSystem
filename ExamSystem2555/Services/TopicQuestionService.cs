using WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace WebApp.Services
{
    public class TopicQuestionService : ITopicQuestionService
    {
        private IAsyncGenericRepository<TopicQuestion> _topicQuestionRepository;

        public TopicQuestionService(IAsyncGenericRepository<TopicQuestion> topicQuestionRepository)
        {
            _topicQuestionRepository = topicQuestionRepository;
        }

        public async Task<TopicQuestion> GetTopicQuestionByIdAsync(int? id)
        {
            return await _topicQuestionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TopicQuestion>> GetAllTopicQuestionsAsync()
        {
            return await _topicQuestionRepository.GetAllAsync();
        }

        public async Task<TopicQuestion> AddTopicQuestionAsync(TopicQuestion topicQuestion)
        {
            return await _topicQuestionRepository.AddAsync(topicQuestion);
        }

        public async Task<TopicQuestion> UpdateTopicQuestionAsync(TopicQuestion topicQuestion)
        {
            return await (_topicQuestionRepository.UpdateAsync(topicQuestion));
        }

        public async Task DeleteTopicQuestionAsync(int? id)
        {
            await _topicQuestionRepository.DeleteAsync(id);
        }

        public async Task<TopicQuestion> AddTopicQuestionAsync(Question question, Topic topic)
        {
            var newTopicQuestion = new TopicQuestion
            {
                Question = question,
                Topic = topic
            };
            await AddTopicQuestionAsync(newTopicQuestion);
            return newTopicQuestion;
        }

    }
}
