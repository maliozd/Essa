using Application.InstaMedias.DTOs;
using MediatR;

namespace Application.InstaMedias.Queries.GetInstaMedia
{
    public class GetInstaMediasQuery : IRequest<List<MediaResponse>>
    {
    }
}
