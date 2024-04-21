using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    //primary constructor
    public class EssaDbContext(DbContextOptions<EssaDbContext> options) : DbContext(options), IEssaDbContext
    {
        public DbSet<InstaMedia> InstaMedias => Set<InstaMedia>();

        public DbSet<RentRequest> RentRequests => Set<RentRequest>();


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.Entity is BaseEntity entity)
                {
                    entity.CreatedDate = DateTime.Now;
                    entity.Status = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
