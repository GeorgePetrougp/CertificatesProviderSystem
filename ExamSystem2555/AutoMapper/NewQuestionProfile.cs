using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models._Questions;

namespace WebApp.AutoMapper
{
    public class NewQuestionProfile:Profile
    {
        public NewQuestionProfile()
        {
            CreateMap<NewQuestionDTO, Question>();
            CreateMap<Question, NewQuestionDTO>();

            CreateMap<NewQuestionPossibleAnswerDTO, QuestionPossibleAnswer>();
            CreateMap<QuestionPossibleAnswer, NewQuestionPossibleAnswerDTO>();


        }
    }
}
