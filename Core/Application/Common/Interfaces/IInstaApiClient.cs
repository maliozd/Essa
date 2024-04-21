using Application.Common.DTOs.InstagramApiDtos;

namespace Application.Common.Interfaces
{
    public interface IInstaApiClient
    {
        Task<List<Node>> GetMediaNodesAsync(string username);
    }

}
