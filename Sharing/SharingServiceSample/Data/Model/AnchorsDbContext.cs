using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharingService.Data.Model
{
    public class AnchorsDbContext: DbContext
    {
        public DbSet<Anchor> Anchors { get; set; }

        public AnchorsDbContext(DbContextOptions<AnchorsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAnchor(modelBuilder.Entity<Anchor>());

        }

        private static void ConfigureAnchor(EntityTypeBuilder<Anchor> builder)
        {
            builder.ToTable("anchors").HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id").IsRequired();
            builder.Property(t => t.Key).HasColumnName("key").IsRequired();
        }
    }
}
