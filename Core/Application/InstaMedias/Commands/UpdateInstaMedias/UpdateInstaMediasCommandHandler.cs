using Application.Common.DTOs.InstagramApiDtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.InstaMedias.Commands.UpdateInstaMedias
{
    public class UpdateInstaMediasCommandHandler : IRequestHandler<UpdateInstaMediasCommand>
    {
        readonly IEssaDbContext _dbContext;
        readonly IInstaApiClient _instaApiClient;
        readonly IMapper _mapper;

        public UpdateInstaMediasCommandHandler(IInstaApiClient instaApiClient, IEssaDbContext dbContext, IMapper mapper)
        {
            _instaApiClient = instaApiClient;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateInstaMediasCommand request, CancellationToken cancellationToken)
        {
            var mediaNodes = await _instaApiClient.GetMediaNodesAsync(request.Username);

            List<InstaMedia> newMedias = new();
            foreach (var node in mediaNodes)
            {
                AddNodeToList(node, newMedias, false);

                foreach (Node childNode in node.ChildNodes)
                {
                    AddNodeToList(childNode, newMedias, true);
                }
            }

            //var newMedias = mediaNodes
            //    .SelectMany(node => new[] { node }.Concat(node.ChildNodes))
            //    .Select(Map)
            //    .Where(media => !_dbContext.InstaMedias.Any(m => m.InstaId == media.InstaId))
            //    .ToList();



            await _dbContext.InstaMedias.AddRangeAsync(newMedias);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        void AddNodeToList(Node node, List<InstaMedia> newMedias, bool isGalleryItem)
        {
            InstaMedia media = Map(node);


            if (!_dbContext.InstaMedias.Any(m => m.InstaId == media.InstaId))
                newMedias.Add(media);
        }
        InstaMedia Map(Node node) => _mapper.Map<InstaMedia>(node);
    }
}
