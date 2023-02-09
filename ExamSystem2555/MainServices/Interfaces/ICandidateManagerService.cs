using MyDatabase.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface ICandidateManagerService
    {
        ICandidateService CandidateService { get; }
        ICandidateAddressService AddressService { get; }
        ICertificateService CertificateService { get; }
        IExaminationService ExaminationService { get; }
        ICandidateExaminationService CandidateExamService { get; }
        IUserCandidateService UserCandidateService { get; }
        Task LoadCandidateAddress(Candidate candidate);
        Task SaveChangesAsync();

    }
}
