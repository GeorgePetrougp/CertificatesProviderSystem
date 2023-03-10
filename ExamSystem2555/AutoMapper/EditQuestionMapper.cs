using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models;
using WebApp.DTO_Models.Final;

namespace WebApp.AutoMapper
{
    public class EditQuestionMapper:Profile
    {

        public EditQuestionMapper()
        {
            CreateMap<Question, EditQuestionView>()
                .ForMember(dest => dest.EditQuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.EditQuestionDisplay, opt => opt.MapFrom(src => src.Display))
                .ReverseMap();


            CreateMap<List<QuestionPossibleAnswer>, List<AnswerView>>()
                .ConvertUsing(src => src.Select(x => new AnswerView
                {
                    QAnswerId = x.QuestionPossibleAnswerId,
                    Answer = x.PossibleAnswer,
                    IsCorrect = x.IsCorrect
                }).ToList());

            CreateMap<QuestionPossibleAnswer, AnswerView>()
            .ForMember(dest => dest.QAnswerId, opt => opt.MapFrom(src => src.QuestionPossibleAnswerId))
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.PossibleAnswer))
            .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect))
            .ReverseMap();

            CreateMap<ICollection<TopicQuestion>, TopicView>()
                        .ForMember(dest => dest.SelectedTopicIds, opt => opt.MapFrom(src => src.Select(x => x.Topic.TopicId)))
                        .ForMember(dest => dest.TopicsList, opt => opt.Ignore())
                        .ReverseMap();

            CreateMap<QuestionPossibleAnswer,QuestionPossibleAnswersDTO>()
                .ForMember(dest=>dest.QuestionPossibleAnswerId,opt=>opt.MapFrom(src=>src.QuestionPossibleAnswerId))
                .ForMember(dest => dest.QuestionPossibleAnswer, opt => opt.MapFrom(src => src.PossibleAnswer))
                .ForMember(dest => dest.IsAnswerCorrect, opt => opt.MapFrom(src => src.IsCorrect))
                .ReverseMap();
        }

            //.ForMember(dest => dest.QuestionDifficulty, opt => opt.MapFrom(src => new QuestionDifficulty { QuestionDifficultyId = src.SelectedDifficultyId }))
            //.ForMember(dest => dest.QuestionDifficulty, opt => opt.MapFrom(src => dbContext.QuestionDifficulties.Find(src.SelectedDifficultyId)))
            

    //CreateMap<ICollection<CertificateTopicQuestion>, CertificatesView>()
    //    .ForMember(dest => dest.SelectedCertificateIds, opt => opt.MapFrom(src => src.Select(x => x.Certificate.CertificateId)))
    //    .ForMember(dest => dest.CertificateList, opt => opt.Ignore())
    //    .ReverseMap();
}
}
