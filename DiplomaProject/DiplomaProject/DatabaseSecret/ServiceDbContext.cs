using DiplomaProject.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace DiplomaProject.DatabaseSecret
{
    public abstract class ServiceDbContext : DbContext
    {
        //register services here like on example
        //public virtual DbSet<Model> Models { get; set; }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public ServiceDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
