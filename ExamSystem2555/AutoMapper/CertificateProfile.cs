using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.Certificates;

namespace WebApp.AutoMapper
{
    public class CertificateProfile:Profile
    {
        public CertificateProfile()
        {
            CreateMap<Certificate,CertificateDTO>();
            CreateMap<CertificateDTO, Certificate>();

            CreateMap<Level, CertificateLevelDTO>();
            CreateMap<CertificateLevelDTO, Level>();

        }
    }
}
