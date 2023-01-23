using MyDatabase.Models;

namespace WebApp.Services
{
    public interface IAddressService
    {
        Task<Address> GetAddressByIdAsync(int? id);
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<Address> AddAddressAsync(Address entity);
        Task<Address> UpdateAddressAsync(Address entity);
        Task DeleteAddressAsync(int? id);
        string CheckNull(Address entity);
    }
}
