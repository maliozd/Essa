using Application.Common.DTOs;

namespace Application.Common.Interfaces
{
    public interface IMailSender
    {/// <summary>
     /// send mail instantly
     /// </summary>
     /// <param name="mailDto"></param>
     /// <param name="cancellationToken"></param>
     /// <returns>
     /// is mail sended
     /// </returns>
        Task<bool> SendAsync(MailDto mailDto, CancellationToken cancellationToken);
    }
}
