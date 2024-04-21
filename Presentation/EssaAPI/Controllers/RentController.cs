using Application.RentRequests.Commands;
using Application.RentRequests.DTOs;
using Application.RentRequests.Queries.GetRentRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EssaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentController(IMediator mediator) : ControllerBase
    {
        readonly IMediator _mediator = mediator;
        [HttpPost]
        public async Task<ActionResult<Guid>> PostRequest(CreateRentRequestCommand createRentRequestCommand)
        {
            var id = await _mediator.Send(createRentRequestCommand);
            return Ok(id);
        }
        [HttpGet]
        public async Task<ActionResult<List<RentRequestDto>>> SGet()
        {
            var response = await _mediator.Send(new GetRentRequestQuery());
            return Ok(response);
        }
    }
}
