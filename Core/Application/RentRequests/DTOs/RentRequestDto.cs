namespace Application.RentRequests.DTOs
{
    public class RentRequestDto
    {
        public string ArrivalPoint { get; set; }
        public int PersonCount { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Message { get; set; }
        public string RequestType { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
