using Domain.Enums;
using MediatR;

namespace Application.RentRequests.Commands
{
    public class CreateRentRequestCommand : IRequest<Guid>
    {
        public string ArrivalPoint { get; set; }
        public int PersonCount { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Message { get; set; }
        public RequestType RequestType { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime RentEndDate { get; set; }
    }
}
