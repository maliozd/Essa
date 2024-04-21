using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.RentRequests.Commands
{
    public class CreateRentRequestCommandHandler(IMapper mapper, IEssaDbContext dbContext, IConfiguration configuration, IMailSender mailSender) : IRequestHandler<CreateRentRequestCommand, Guid>
    {
        readonly IEssaDbContext _dbContext = dbContext;
        readonly IMapper _mapper = mapper;
        readonly IConfiguration _configuration = configuration;
        readonly IMailSender _mailSender = mailSender;
        public async Task<Guid> Handle(CreateRentRequestCommand request, CancellationToken cancellationToken)
        {
            RentRequest rentRequest = _mapper.Map<RentRequest>(request); //TODO : Create RequestType mapping

            string mailContent = GetReplacedMailContent(rentRequest);
            string[] participantArray = [$"{_configuration["CompanyMail"]}", "alperenerturk14@gmail.com", "helixsoftware.tr@gmail.com", "ysn.ayg@gmail.com"];

            MailDto mail = new(participantArray, "Yeni Kiralama Formu Gönderimi", mailContent);

            bool isSended = await _mailSender.SendAsync(mail, cancellationToken);
            rentRequest.InformationMailSent = isSended;

            await _dbContext.RentRequests.AddAsync(rentRequest, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return rentRequest.Id;
        }

        static string GetReplacedMailContent(RentRequest request)
        {
            string mailContent = @"<!DOCTYPE html>
                     <html lang=""tr"">
                     <head>
                         <meta charset=""UTF-8"">
                         <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                         <title>Kiralama İsteği Bilgileri</title>
                     </head>
                     <body>
                         <h2 style=""margin-bottom: 20px;"">Kiralama İsteği Bilgileri</h2>
                         <p style=""font-size: 16px; color: #666666;"">Aşağıda, kiralama isteği ile ilgili bilgiler yer almaktadır:</p>
                         <table style=""width: 100%; border-collapse: collapse;"">
                           
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Varış Noktası</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{ArrivalPoint}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Kişi Sayısı</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{PersonCount}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Ad Soyad</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{NameSurname}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">E-posta</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{Email}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Telefon</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{Phone}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Mesaj</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left; ""><b>{{Message}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">İstek Türü</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{RequestType}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Kiralama Başlangıç Tarihi</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{RentStartDate}}</b></td>
                             </tr>
                             <tr>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;"">Kiralama Bitiş Tarihi</td>
                                 <td style=""border: 1px solid #dddddd; padding: 8px; text-align: left;""><b>{{RentEndDate}}</b></td>
                             </tr>
                         </table>
                     </body>
                     </html>";
            mailContent = mailContent.Replace("{{ArrivalPoint}}", request.ArrivalPoint);
            mailContent = mailContent.Replace("{{PersonCount}}", request.PersonCount.ToString());
            mailContent = mailContent.Replace("{{NameSurname}}", request.NameSurname);
            mailContent = mailContent.Replace("{{Email}}", request.Email);
            mailContent = mailContent.Replace("{{Phone}}", request.Phone);
            mailContent = mailContent.Replace("{{Message}}", request.Message ?? "");
            mailContent = mailContent.Replace("{{RequestType}}", request.RequestType.ToString()); //TODO : Convert to RequestType string
            mailContent = mailContent.Replace("{{RentStartDate}}", request.RentStartDate.ToString());
            mailContent = mailContent.Replace("{{RentEndDate}}", request.RentEndDate.ToString());

            return mailContent;
        }
    }
}
