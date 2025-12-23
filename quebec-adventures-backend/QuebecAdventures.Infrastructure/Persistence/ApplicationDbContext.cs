using Microsoft.EntityFrameworkCore;
using QuebecAdventures.Domain.Entities;

namespace QuebecAdventures.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Activity> Activities { get; set; }
	public DbSet<Review> Reviews { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Configuration de l'entité Activity
		modelBuilder.Entity<Activity>(entity =>
		{
			// Conversion des Enums en String pour la DB
			entity.Property(e => e.Type).HasConversion<string>();
			entity.Property(e => e.Region).HasConversion<string>();
			entity.Property(e => e.PriceRange).HasConversion<string>();
			entity.Property(e => e.Difficulty).HasConversion<string>();
		});
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker.Entries<Activity>();

		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.UtcNow;
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
			else if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
		}

		return base.SaveChangesAsync(cancellationToken);
	}
}
