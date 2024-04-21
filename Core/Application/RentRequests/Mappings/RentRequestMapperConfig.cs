using Application.RentRequests.Commands;
using Application.RentRequests.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.RentRequests.Mappings
{
    public class RentRequestMapperConfig : Profile
    {
        public RentRequestMapperConfig()
        {
            CreateMap<CreateRentRequestCommand, RentRequest>();

            CreateMap<RentRequest, RentRequestDto>().
                ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.RequestType.ConvertToString()));
        }
    }
}
