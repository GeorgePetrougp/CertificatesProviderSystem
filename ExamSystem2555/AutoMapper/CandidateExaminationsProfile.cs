using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.CandidateExaminations;

namespace WebApp.AutoMapper
{
    public class CandidateExaminationsProfile:Profile
    {
        public CandidateExaminationsProfile()
        {
            CreateMap<CandidateExam,CandidateExaminationsDTO>().ReverseMap();
            CreateMap<CandidateExamResults, CandidateExaminationResultsDTO>().ReverseMap();

        }
    }
}
