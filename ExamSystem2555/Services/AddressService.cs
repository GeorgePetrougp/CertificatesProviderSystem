using WebApp.Repositories;
using MyDatabase.Models;
using System.Net;

namespace WebApp.Services
{
    public class AddressService : IAddressService
    {
        private IAsyncGenericRepository<Address> _addressRepository;

        public AddressService(IAsyncGenericRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetAddressByIdAsync(int? id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            return await _addressRepository.AddAsync(address);
        }

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            return await (_addressRepository.UpdateAsync(address));
        }

        public async Task DeleteAddressAsync(int? id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        public string CheckNull(Address address)
        {
            if (address != null)
            {
                return "OK";
            }

            return null;
        }
    }
}
