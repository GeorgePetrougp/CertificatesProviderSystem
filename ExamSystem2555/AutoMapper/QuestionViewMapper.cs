using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models;

namespace WebApp.Mappers
{
    public class QuestionViewMapper : Profile
    {
        public QuestionViewMapper()
        {

            CreateMap<Question, QuestionView>()
                .ForMember(dest => dest.QId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.QDisplay, opt => opt.MapFrom(src => src.Display))
                .ReverseMap();

            //CreateMap<QuestionDifficulty, QuestionDifficultyView>()
            //    .ForMember(dest => dest.SelectedId, opt => opt.MapFrom(src => src.QuestionDifficultyId))
            //    .ForMember(dest => dest.Difficulties, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<ICollection<QuestionPossibleAnswer>, AnswerView>()
            //    .ForMember(dest => dest.SelectedPossibleAnswerIds, opt => opt.MapFrom(src => src.Select(x => x.QuestionPossibleAnswerId)))
            //    .ForMember(dest => dest.QuestionPossibleAnswers, opt => opt.MapFrom(src => src))
            //    .ReverseMap();

            //CreateMap<ICollection<TopicQuestion>, TopicView>()
            //    .ForMember(dest => dest.SelectedTopicIds, opt => opt.MapFrom(src => src.Select(x => x.Topic.TopicId)))
            //    .ForMember(dest => dest.TopicsList, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<ICollection<CertificateTopicQuestion>, CertificatesView>()
            //    .ForMember(dest => dest.SelectedCertificateIds, opt => opt.MapFrom(src => src.Select(x => x.Certificate.CertificateId)))
            //    .ForMember(dest => dest.CertificateList, opt => opt.Ignore())
            //    .ReverseMap();
        }
    }

}
