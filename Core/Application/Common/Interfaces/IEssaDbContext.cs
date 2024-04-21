using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.Common.Interfaces
{
    public interface IEssaDbContext
    {
        DbSet<InstaMedia> InstaMedias { get; }
        DbSet<RentRequest> RentRequests { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
