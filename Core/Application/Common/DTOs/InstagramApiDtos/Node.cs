namespace Application.Common.DTOs.InstagramApiDtos
{
    public class Node //post
    {
        public string Id { get; set; }
        public string MediaType { get; set; }
        public string Caption { get; set; }
        public string Shortcode { get; set; }
        public Dimensions Dimensions { get; set; }
        public string DisplayUrl { get; set; }
        public bool WillBePost { get; set; } = false;
        public List<ThumbnailResource> ThumbnailResources { get; set; }
        public List<Node> ChildNodes { get; set; } = new();
    }

}
