using MyDatabase.Models;

namespace WebApp.Services.Interfaces
{
    public interface ITopicQuestionService
    {
        Task<TopicQuestion> GetTopicQuestionByIdAsync(int? id);
        Task<IEnumerable<TopicQuestion>> GetAllTopicQuestionsAsync();
        Task<TopicQuestion> AddTopicQuestionAsync(TopicQuestion topicQuestion);
        Task<TopicQuestion> AddTopicQuestionAsync(Topic topic, Question question);
        Task<TopicQuestion> UpdateTopicQuestionAsync(TopicQuestion topicQuestion);
        Task<IEnumerable<TopicQuestion>> AddRangeTopicQuestionAsync(IEnumerable<TopicQuestion> topicQuestions);
        Task DeleteTopicQuestionAsync(int? id);
    }
}
