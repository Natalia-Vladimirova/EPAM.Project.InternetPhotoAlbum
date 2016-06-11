using System.Data.Entity;

namespace ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("PhotoAlbumDb") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(t => t.Users)
                .Map(m =>
                   {
                       m.ToTable("UserRoles");
                       m.MapLeftKey("UserId");
                       m.MapRightKey("RoleId");
                   });
        }
    }
}
