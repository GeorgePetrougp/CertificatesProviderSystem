using AutoMapper;
using MyDatabase.Models;
using WebApp.DTO_Models.Candidates;
using WebApp.DTO_Models.Final;

namespace WebApp.AutoMapper
{
    public class CandidateProfile:Profile
    {
        public CandidateProfile() 
        {
            CreateMap<Candidate, CandidateDTO>();
            CreateMap<CandidateDTO, Candidate>();
        }
    }
}
