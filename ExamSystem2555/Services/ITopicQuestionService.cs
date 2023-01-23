using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ITopicQuestionService
    {
        Task<TopicQuestion> GetTopicQuestionByIdAsync(int? id);
        Task<IEnumerable<TopicQuestion>> GetAllTopicQuestionsAsync();
        Task<TopicQuestion> AddTopicQuestionAsync(TopicQuestion topicQuestion);
        Task<TopicQuestion> UpdateTopicQuestionAsync(TopicQuestion topicQuestion);
        Task DeleteTopicQuestionAsync(int? id);
        Task<TopicQuestion> AddTopicQuestionAsync(Question question, Topic topic);
    }
}
