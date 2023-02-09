using WebApp.Services;

namespace WebApp.MainServices.Interfaces
{
    public interface IEShopService
    {
        public ICandidateService CandidateService { get; }

        public ICertificateService CertificateService { get; }

        public ICandidateExamService CandidateExamService { get; }

        public IExaminationService ExaminationService { get; }
        Task SaveChangesAsync();


    }
}
