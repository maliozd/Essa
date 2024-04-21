using Application.Common.Interfaces;
using Application.InstaMedias.DTOs;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InstaMedias.Queries.GetInstaMedia
{
    public class GetInstaMediasQueryHandler : IRequestHandler<GetInstaMediasQuery, List<MediaResponse>>
    {
        readonly IEssaDbContext _dbContext;
        readonly IMapper _mapper;
        public GetInstaMediasQueryHandler(IEssaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<MediaResponse>> Handle(GetInstaMediasQuery request, CancellationToken cancellationToken)
        {
            var data = await _dbContext.InstaMedias.
                   Where(media => media.MediaType == MediaType.Image && media.Status).
                   ToListAsync();

            var returnData = _mapper.Map<List<MediaResponse>>(data);
            return returnData;

            //.Skip((page - 1) * size) 
            //.Take(size) 
        }
    }
}
