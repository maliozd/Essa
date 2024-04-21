using Application.InstaMedias.Commands.UpdateInstaMedias;
using MediatR;
using Quartz;

namespace EssaAPI.QuartzJpbs
{
    public class UpdateMediaJob : IJob
    {
        readonly IMediator _mediator;
        public UpdateMediaJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new UpdateInstaMediasCommand());
        }
    }
}
