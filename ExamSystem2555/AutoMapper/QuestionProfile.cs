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
        }
    }
}
