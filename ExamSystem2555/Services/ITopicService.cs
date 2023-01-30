using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ITopicService
    {
        Task<Topic> GetTopicByIdAsync(int? id);
        Task<IEnumerable<Topic>> GetAllTopicsAsync();
        Task<Topic> AddTopicAsync(Topic entity);
        Task<Topic> UpdateTopicAsync(Topic entity);
        Task DeleteTopicAsync(int? id);
        Task<IEnumerable<Topic>> SortTopicsById(IEnumerable<int> topicIds);

        string CheckNull(Topic entity);
    }
}
