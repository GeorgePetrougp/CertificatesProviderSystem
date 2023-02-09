using WebApp.Repositories;
using MyDatabase.Models;
using System.Net;

namespace WebApp.Services
{
    public class CandidateAddressService : ICandidateAddressService
    {
        private IAsyncGenericRepository<CandidateAddress> _addressRepository;

        public CandidateAddressService(IAsyncGenericRepository<CandidateAddress> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<CandidateAddress> GetAddressByIdAsync(int? id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CandidateAddress>> GetAllAddressesAsync()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<CandidateAddress> AddAddressAsync(CandidateAddress address)
        {
            return await _addressRepository.AddAsync(address);
        }

        public async Task<CandidateAddress> UpdateAddressAsync(CandidateAddress address)
        {
            return await (_addressRepository.UpdateAsync(address));
        }

        public async Task DeleteAddressAsync(int? id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        public string CheckNull(CandidateAddress address)
        {
            if (address != null)
            {
                return "OK";
            }

            return null;
        }
    }
}
