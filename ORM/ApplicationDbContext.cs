using System.Data.Entity;

namespace ORM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("InetPhotoAlbumDb") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

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

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ratings)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photo>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Photo)
                .WillCascadeOnDelete(true);
        }
    }
}
