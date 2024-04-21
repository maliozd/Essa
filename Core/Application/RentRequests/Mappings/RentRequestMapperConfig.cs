using Application.RentRequests.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.RentRequests.Mappings
{
    public class RentRequestMapperConfig : Profile
    {
        public RentRequestMapperConfig()
        {
            CreateMap<CreateRentRequestCommand, RentRequest>();
        }
    }
}
