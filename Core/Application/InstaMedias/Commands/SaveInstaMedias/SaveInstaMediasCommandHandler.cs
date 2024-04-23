using Application.Common.DTOs.InstagramApiDtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.InstaMedias.Commands.SaveInstaMedias
{
    public class SaveInstaMediasCommandHandler(IInstaApiClient _instaApi, IMapper _mapper, IEssaDbContext _dbContext) : IRequestHandler<SaveInstaMediasCommand>
    {

        public async Task Handle(SaveInstaMediasCommand request, CancellationToken cancellationToken)
        {
            var mediaNodes = await _instaApi.GetMediaNodesAsync(request.Username);

            foreach (Node node in mediaNodes)
            {
                InstaMedia? media = Map(node);
                await AddMediaAsync(node, false, string.Empty);

                foreach (var child in node.ChildNodes)
                    await AddMediaAsync(child, true, media.InstaId);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        async Task AddMediaAsync(Node node, bool isGalleryItem, string parentInstaId)
        {
            InstaMedia? media = Map(node);

            if (_dbContext.InstaMedias.FirstOrDefault(m => m.InstaId == media.InstaId) is not null)
                return;

            media.IsGalleryItem = isGalleryItem;
            media.ParentInstaId = parentInstaId;
            await _dbContext.InstaMedias.AddAsync(media);
        }

        InstaMedia Map(Node node) => _mapper.Map<InstaMedia>(node);

    }
}
