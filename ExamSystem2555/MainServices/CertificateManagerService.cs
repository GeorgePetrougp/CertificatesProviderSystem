using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDatabase.Models;
using WebApp.Data;
using WebApp.DTO_Models;
using WebApp.DTO_Models.Certificates;
using WebApp.MainServices.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.MainServices
{
    public class CertificateManagerService : ICertificateManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICertificateService _certificateService;
        private readonly ICertificateTopicService _certificateTopicService;
        private readonly ICertificateLevelService _levelService;

        public CertificateManagerService(ApplicationDbContext context, IMapper mapper, ICertificateService certificateService, ICertificateTopicService certificateTopicService, ICertificateLevelService levelService)
        {
            _context = context;
            _mapper = mapper;
            _certificateService = certificateService;
            _certificateTopicService = certificateTopicService;
            _levelService = levelService;
        }
        public ICertificateService CertificateService { get => _certificateService; }
        public ICertificateTopicService CertificateTopicService { get => _certificateTopicService; }
        public ICertificateLevelService LevelService { get => _levelService; }

        public async Task LoadLevel(Certificate certificate)
        {
            await _context.Entry(certificate).Reference(c => c.Level).LoadAsync();
        }

        public async Task LoadTopics(Certificate certificate)
        {
            await _context.Entry(certificate).Collection(c => c.CertificateTopics).LoadAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CertificateDTO>> CreateCertificateDTOs()
        {
            var certificatesList = (await _certificateService.GetAllCertificatesAsync()).ToList();

            //certificatesList.ForEach(async c => await LoadLevel(c));
            foreach (var cert in certificatesList)
            {
                await LoadLevel(cert);
            }
            var certificateDTOsList = _mapper.Map<List<CertificateDTO>>(certificatesList);

            return certificateDTOsList;
        }

        public async Task<CertificateDTO> CreateCertificateDTO(int? id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            await LoadLevel(certificate);

            var certificateDTO = _mapper.Map<CertificateDTO>(certificate);

            return certificateDTO;
        }

        public async Task<CreateCertificateView> CreateCertificateView()
        {
            var levelList = await _levelService.GetAllLevelsAsync();
            var certificateView = new CreateCertificateView
            {
                CertificateLevelsList = await CreateSelectList(levelList)
            };

            return certificateView;
        }

        public async Task<CreateCertificateView> CreateCertificateView(int? id)
        {
            var certificateView = await CreateCertificateView();
            certificateView.CertificateDTO = await CreateCertificateDTO(id);
            certificateView.SelectedLevelId = certificateView.CertificateDTO.Level.LevelId;

            return certificateView;
        }

        public async Task<CreateCertificateView> EditCertificateView(int? id)
        {
            var certificateView = await CreateCertificateView(id);

            return certificateView;
        }

        public async Task<Certificate> TBD(CreateCertificateView model)
        {
            var newCertificate = await Task.Run(() =>_mapper.Map<Certificate>(model.CertificateDTO));
            newCertificate.Level = await _levelService.GetLevelByIdAsync(model.SelectedLevelId);

            return newCertificate;
        }

        public async Task<Certificate> AddCertificate(Certificate certificate)
        {
            await _certificateService.AddCertificateAsync(certificate);
            await SaveChangesAsync();

            return certificate;
        }

        public async Task<Certificate> UpdateCertificate(Certificate certificate)
        {
            await _certificateService.UpdateCertificateAsync(certificate);
            await SaveChangesAsync();

            return certificate;
        }

        //helpers
        public async Task<SelectList> CreateSelectList(IEnumerable<CertificateLevel> levels)
        {
            var levelDTOList = await Task.Run(() => _mapper.Map<List<CertificateLevel>>(levels));

            return new SelectList(levelDTOList, "LevelId", "Title");
        }

        public async Task<bool> NullValidation(int? id)
        {
            var certificatesList = await _certificateService.GetAllCertificatesAsync();
            var certificate = await _certificateService.GetCertificateByIdAsync(id);

            if (id == null || certificatesList == null || certificate == null)
            {
                return true;
            }

            return false;

        }



    }
}
