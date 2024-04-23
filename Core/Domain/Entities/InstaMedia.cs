using Domain.Enums;

namespace Domain.Entities
{

    public class InstaMedia : BaseEntity
    {
        public string DisplayUrl { get; set; }
        public MediaType MediaType { get; set; }
        public string Shortcode { get; set; }
        public string InstaId { get; set; }
        public string Caption { get; set; }
        public bool IsGalleryItem { get; set; }
        public string? ParentInstaId { get; set; }
    }
}
