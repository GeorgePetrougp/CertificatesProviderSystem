using MyDatabase.Models;

namespace WebApp.Services
{
    public interface ICandidateAddressService
    {
        Task<CandidateAddress> GetAddressByIdAsync(int? id);
        Task<IEnumerable<CandidateAddress>> GetAllAddressesAsync();
        Task<CandidateAddress> AddAddressAsync(CandidateAddress entity);
        Task<CandidateAddress> UpdateAddressAsync(CandidateAddress entity);
        Task DeleteAddressAsync(int? id);
        string CheckNull(CandidateAddress entity);
    }
}
