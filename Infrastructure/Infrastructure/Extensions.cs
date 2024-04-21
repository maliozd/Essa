using System.Net.Mail;

namespace Infrastructure
{
    public static class Extensions
    {
        public static void AddRange(this MailAddressCollection mailAddresses, string[] recipants)
        {
            foreach (string item in recipants)
                mailAddresses.Add(new MailAddress(item));
        }
    }
}
