using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyDatabase.Models;
using WebApp.DTO_Models.Candidates;

namespace WebApp.AutoMapper
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

        }
    }
}
