using MediatR;

namespace Application.InstaMedias.Commands.UpdateInstaMedias
{
    public class UpdateInstaMediasCommand : IRequest
    {
        public UpdateInstaMediasCommand(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
