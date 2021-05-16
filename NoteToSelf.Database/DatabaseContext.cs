using Microsoft.EntityFrameworkCore;
using NoteToSelf.Database.Models;

namespace NoteToSelf.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserProfile> Users { get; internal set; } = null;

        public DatabaseContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>(x => {
                x.HasMany(y => y.Notes)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

        public override void Dispose()
        {
            SaveChanges();
            base.Dispose();
        }
    }
}
