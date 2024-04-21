using Application.RentRequests.Commands;
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
    }
}
