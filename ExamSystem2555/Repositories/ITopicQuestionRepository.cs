using MyDatabase.Models;

namespace WebApp.Repositories
{
    public interface ITopicQuestionRepository:IAsyncGenericRepository<TopicQuestion>
    {
        Task<TopicQuestion> AddTopicQuestionAsync(Question question, Topic topic);
    }
}
