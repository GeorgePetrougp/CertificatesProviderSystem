using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.Final.Views;

namespace WebApp.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<QuestionPossibleAnswer, QuestionPossibleAnswerDTO>();
            CreateMap<QuestionPossibleAnswerDTO, QuestionPossibleAnswer>();
        }
    }
}
