using AutoMapper;
using WebApp.DTO_Models.CertificateExaminations;
using MyDatabase.Models;

namespace WebApp.AutoMapper
{
    public class CertificateExaminationProfile:Profile
    {
        public CertificateExaminationProfile()
        {
            CreateMap<Examination, CertificateExaminationDTO>();
            CreateMap<CertificateExaminationDTO, Examination>();

        }
    }
}
