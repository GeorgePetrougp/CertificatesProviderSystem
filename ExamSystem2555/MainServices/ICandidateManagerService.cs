using MyDatabase.Models;
using WebApp.Services;

namespace WebApp.MainServices
{
    public interface ICandidateManagerService
    {
        ICandidateService CandidateService { get; }
        IAddressService AddressService { get; }
        ICertificateService CertificateService { get; }
        IExaminationService ExaminationService { get; }
        ICandidateExamService CandidateExamService { get; }
        IUserCandidateService UserCandidateService { get; }
        Task LoadCandidateAddress(Candidate candidate);
        Task SaveChangesAsync();

    }
}
