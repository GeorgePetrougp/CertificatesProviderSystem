using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.CandidateExaminations;

namespace WebApp.AutoMapper
{
    public class CandidateExaminationsProfile:Profile
    {
        public CandidateExaminationsProfile()
        {
            CreateMap<CandidateExamination,CandidateExaminationsDTO>().ReverseMap();
            CreateMap<CandidateExaminationResult, CandidateExaminationResultsDTO>().ReverseMap();

        }
    }
}
