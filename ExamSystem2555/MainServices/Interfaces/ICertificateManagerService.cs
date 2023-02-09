using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.DTO_Models.Certificates;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices.Interfaces
{
    public interface ICertificateManagerService
    {
        public ICertificateService CertificateService { get; }
        public ICertificateTopicService CertificateTopicService { get; }
        public ICertificateLevelService LevelService { get; }
        Task<IEnumerable<CertificateDTO>> CreateCertificateDTOs();
        Task<CertificateDTO> CreateCertificateDTO(int? id);
        Task LoadTopics(Certificate certificate);
        Task LoadLevel(Certificate certificate);
        Task<bool> NullValidation(int? id);
        Task<CreateCertificateView> CreateCertificateView();
        Task<CreateCertificateView> CreateCertificateView(int? id);
        Task<SelectList> CreateSelectList(IEnumerable<CertificateLevel> levels);
        Task<Certificate> TBD(CreateCertificateView model);
        Task<Certificate> AddCertificate(Certificate certificate);
        Task<Certificate> UpdateCertificate(Certificate certificate);
        Task SaveChangesAsync();

    }
}
