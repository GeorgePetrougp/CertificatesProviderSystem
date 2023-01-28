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

        public async Task<TopicQuestion> GetTopicQuestionByIdAsync(int? id) => await _topicQuestionRepository.GetByIdAsync(id);

        public async Task<IEnumerable<TopicQuestion>> GetAllTopicQuestionsAsync() => await _topicQuestionRepository.GetAllAsync();

        public async Task<TopicQuestion> AddTopicQuestionAsync(TopicQuestion topicQuestion) => await _topicQuestionRepository.AddAsync(topicQuestion);

        public async Task<TopicQuestion> UpdateTopicQuestionAsync(TopicQuestion topicQuestion) => await _topicQuestionRepository.UpdateAsync(topicQuestion);

        public async Task DeleteTopicQuestionAsync(int? id) => await _topicQuestionRepository.DeleteAsync(id);

        public async Task<TopicQuestion> AddTopicQuestionAsync(Topic topic, Question question)
        {
            var newTopicQuestion = new TopicQuestion { Question = question, Topic = topic };
            await AddTopicQuestionAsync(newTopicQuestion);
            return newTopicQuestion;
        }

        public async Task<TopicQuestion> AddTopicQuestionAsync(Question question)
        {
            var newTopicQuestion = new TopicQuestion{Question = question};
            await AddTopicQuestionAsync(newTopicQuestion);
            return newTopicQuestion;
        }

        public async Task<IEnumerable<TopicQuestion>> AddRangeTopicQuestionAsync(IEnumerable<TopicQuestion> topicQuestions)
        {
            var topicQuestionsList = topicQuestions;
            await _topicQuestionRepository.AddRangeAsync(topicQuestionsList);
            return topicQuestionsList;
        }
    }
}
