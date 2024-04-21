using MediatR;

namespace Application.InstaMedias.Commands.SaveInstaMedias
{
    public class SaveInstaMediasCommand : IRequest
    {
        public string Username { get; set; }
    }
}
