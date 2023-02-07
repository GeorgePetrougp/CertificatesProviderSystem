using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface ICandidateManagerService
    {
        ICandidateService CandidateService { get; }
        IAddressService AddressService { get; }
        Task LoadCandidateAddress(Candidate candidate);
        Task SaveChangesAsync();

    }
}
