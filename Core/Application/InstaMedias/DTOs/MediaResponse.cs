namespace Application.InstaMedias.DTOs
{
    public class MediaResponse(Guid id, string displayUrl, string caption, string shortCode)
    {
        public Guid Id { get; set; } = id;
        public string DisplayUrl { get; set; } = displayUrl;
        public string UrlShortCode = shortCode;
        public string Caption { get; set; } = caption;
    }
}
