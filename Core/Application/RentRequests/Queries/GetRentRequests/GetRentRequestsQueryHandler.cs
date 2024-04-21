using Application.Common.Interfaces;
using Application.RentRequests.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.RentRequests.Queries.GetRentRequests
{
    public class GetRentRequestsQueryHandler(IMapper mapper, IEssaDbContext dbContext, IConfiguration configuration) : IRequestHandler<GetRentRequestQuery, List<RentRequestDto>>
    {
        readonly IEssaDbContext _dbContext = dbContext;
        readonly IMapper _mapper = mapper;
        public async Task<List<RentRequestDto>> Handle(GetRentRequestQuery request, CancellationToken cancellationToken)
        {
            var data = await _dbContext.RentRequests.ToListAsync(cancellationToken);
            var returnData = _mapper.Map<List<RentRequestDto>>(data).OrderByDescending(x => x.CreatedDate).ToList();

            return returnData;
        }
    }
}
