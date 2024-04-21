namespace Infrastructure.Mail
{
    public class MailConfiguration
    {
        public ProtocolsConfiguration Protocols { get; set; }
        public CredentialsConfiguration Credentials { get; set; }
    }

    public class ProtocolsConfiguration
    {
        public ImapConfiguration Imap { get; set; }
        public SmtpConfiguration Smtp { get; set; }
    }

    public class ImapConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
    }

    public class SmtpConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
    }

    public class CredentialsConfiguration
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
