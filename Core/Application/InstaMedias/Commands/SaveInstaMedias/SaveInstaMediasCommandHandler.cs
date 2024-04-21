using Application.Common.DTOs.InstagramApiDtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.InstaMedias.Commands.SaveInstaMedias
{
    public class SaveInstaMediasCommandHandler(IInstaApiClient instaApi, IMapper mapper, IEssaDbContext dbContext) : IRequestHandler<SaveInstaMediasCommand>
    {
        readonly IInstaApiClient _instaApi = instaApi;
        readonly IMapper _mapper = mapper;
        readonly IEssaDbContext _dbContext = dbContext;

        public async Task Handle(SaveInstaMediasCommand request, CancellationToken cancellationToken)
        {
            var mediaNodes = await _instaApi.GetMediaNodesAsync(request.Username);

            foreach (Node node in mediaNodes)
            {
                InstaMedia? media = _mapper.Map<InstaMedia>(node);
                await _dbContext.InstaMedias.AddAsync(media);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
