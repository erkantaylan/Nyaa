using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Database.Models;

public abstract class SourceDbContext<T> : DbContext
    where T : DbContext
{
    protected SourceDbContext(DbContextOptions<T> options) : base(options) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        IEnumerable<object> modifiedEntries = ChangeTracker
                                             .Entries()
                                             .Where(x => x.State == EntityState.Modified)
                                             .Select(x => x.Entity);

        foreach (object modifiedEntry in modifiedEntries)
        {
            if (modifiedEntry is IEntity auditableEntity)
            {
                auditableEntity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }

        IEnumerable<EntityEntry> deletedEntries = ChangeTracker
                                                 .Entries()
                                                 .Where(x => x.State == EntityState.Deleted);

        foreach (EntityEntry entry in deletedEntries)
        {
            if (entry.Entity is IEntity entity)
            {
                entity.DeletedAt = DateTimeOffset.UtcNow;
                entry.State = EntityState.Modified;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
