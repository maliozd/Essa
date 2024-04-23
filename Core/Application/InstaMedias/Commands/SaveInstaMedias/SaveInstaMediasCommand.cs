using MediatR;

namespace Application.InstaMedias.Commands.SaveInstaMedias
{
    public class SaveInstaMediasCommand : IRequest
    {
        public SaveInstaMediasCommand(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
