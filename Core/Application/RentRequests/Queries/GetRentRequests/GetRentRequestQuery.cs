using Application.RentRequests.DTOs;
using MediatR;

namespace Application.RentRequests.Queries.GetRentRequests
{
    public class GetRentRequestQuery : IRequest<List<RentRequestDto>>
    {
    }
}
