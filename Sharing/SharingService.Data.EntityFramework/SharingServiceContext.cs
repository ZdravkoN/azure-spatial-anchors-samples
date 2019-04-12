using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharingService.Data.Model;

namespace SharingService.Data.EntityFramework
{
    public class SharingServiceContext: DbContext
    {
        public DbSet<Anchor> Anchors { get; set; }

        public SharingServiceContext(DbContextOptions<SharingServiceContext> options): base(options)
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
            builder.Property(t => t.Name).HasColumnName("name").IsRequired();
            builder.Property(t => t.Longitude).HasColumnName("longitude");
            builder.Property(t => t.Latitude).HasColumnName("latitude");
        }
    }
}
