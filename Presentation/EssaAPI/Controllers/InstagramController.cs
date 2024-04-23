using Application.InstaMedias.Commands.SaveInstaMedias;
using Application.InstaMedias.DTOs;
using Application.InstaMedias.Queries.GetInstaMedia;
using EssaAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EssaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstagramController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ApiResponse<List<MediaResponse>>> GetMedias()
        {
            var response = await _mediator.Send(new GetInstaMediasQuery());
            return ApiResponse<List<MediaResponse>>.Success(response);
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<List<MediaResponse>>> GetMediasByUsername(string username)
        {
            //List<Node> mediaNodes = await _instaApi.GetMediaNodesAsync(username);
            //List<InstaMedia> mediaToReturn = _mapper.Map<List<InstaMedia>>(mediaNodes);
            return Ok(new());
        }
        [HttpPost]
        public async Task<ActionResult> SaveInstaMedias(string username)
        {
            await _mediator.Send(new SaveInstaMediasCommand(username));
            return Ok();
        }
    }
}