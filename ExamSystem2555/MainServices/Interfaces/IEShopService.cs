using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface IEShopService
    {
        public ICandidateService CandidateService { get; }

        public ICertificateService CertificateService { get; }

        public ICandidateExaminationService CandidateExamService { get; }

        public IExaminationService ExaminationService { get; }
        Task SaveChangesAsync();


    }
}
