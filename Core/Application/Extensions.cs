using Domain.Enums;
using System.Net.Mail;

namespace Application
{
    public static class Extensions
    {
        public static string ConvertToString(this RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Kira:
                    return "Kiralama";
                case RequestType.İmalat:
                    return "İmalat";
                case RequestType.Satis:
                    return "Satış";
                default:
                    return string.Empty;
            }
        }
        public static void AddRange(this MailAddressCollection mailAddresses, string[] recipants)
        {
            foreach (string item in recipants)
                mailAddresses.Add(new MailAddress(item));
        }
    }
}
