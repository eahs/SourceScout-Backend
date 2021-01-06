using ADSBackend.Models;
using ADSBackend.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADSBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostTag> PostTag { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            builder.Entity<PostTag>()
                .HasOne(pc => pc.Post)
                .WithMany(t => t.Tags)
                .HasForeignKey(cm => cm.PostId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
