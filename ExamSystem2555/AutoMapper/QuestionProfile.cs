using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.Questions;

namespace WebApp.AutoMapper
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question,QuestionDTO>().ReverseMap();
            CreateMap<Question, CreateQuestionDTO>().ReverseMap();

            CreateMap<QuestionDifficulty, QuestionDifficultyDTO>().ReverseMap();
            CreateMap<QuestionPossibleAnswer, QuestionPossibleAnswersDTO>().ReverseMap();

            CreateMap<NewCreateQuestionView, CreateQuestionDTO>();
            CreateMap<QuestionPossibleAnswersDTO, QuestionPossibleAnswer>().ForMember(dest => dest.QuestionPossibleAnswerId, opt => opt.Ignore());
            //CreateMap<QuestionPossibleAnswersDTO, QuestionPossibleAnswer>();


        }
    }
}
