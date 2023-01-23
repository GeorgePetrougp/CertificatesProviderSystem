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
        string CheckNull(Topic entity);
    }
}
