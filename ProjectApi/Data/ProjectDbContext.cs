namespace ProjectApi.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ProjectApi.Data.Models;

    public class ProjectDbContext : IdentityDbContext<User>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }

        public DbSet<AdvertisementImage> AdvertisementImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Advertisement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Advertisement>()
                .HasOne(a => a.AdvertisementCategory)
                .WithMany(ac => ac.Advertisements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AdvertisementImage>()
                .HasOne(ai => ai.Advertisement)
                .WithMany(a => a.AdvertisementImages)
                .HasForeignKey(ai => ai.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
