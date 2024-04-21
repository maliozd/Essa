using Application.Common.Interfaces;
using AutoMapper;
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

        }
    }
}
