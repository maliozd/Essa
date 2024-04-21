namespace Application.Common.DTOs
{
    public class MailDto
    {
        public MailDto(string[] emails, string subject, string body)
        {
            Recipants = emails;
            Subject = subject;
            Body = body;
        }
        public MailDto()
        {

        }

        public string[] Recipants { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
