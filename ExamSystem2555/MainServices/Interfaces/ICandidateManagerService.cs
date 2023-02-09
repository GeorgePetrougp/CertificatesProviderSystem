using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices.Interfaces
{
    public interface ICandidateManagerService
    {
        ICandidateService CandidateService { get; }
        ICandidateAddressService AddressService { get; }
        ICertificateService CertificateService { get; }
        IExaminationService ExaminationService { get; }
        ICandidateExamService CandidateExamService { get; }
        IUserCandidateService UserCandidateService { get; }
        Task LoadCandidateAddress(Candidate candidate);
        Task SaveChangesAsync();

    }
}
