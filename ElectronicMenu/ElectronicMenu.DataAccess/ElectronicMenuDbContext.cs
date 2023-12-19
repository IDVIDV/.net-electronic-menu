using ElectronicMenu.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicMenu.DataAccess
{
    public class ElectronicMenuDbContext : DbContext
    {
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TableEntity> Tables { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<PositionInOrderEntity> PositionsInOrders { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public ElectronicMenuDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
            modelBuilder.Entity<UserRoleEntity>().ToTable("user_roles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_role_claims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();

            modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AdminEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<TableEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TableEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<PositionEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<PositionEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<PositionInOrderEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<PositionInOrderEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<PositionInOrderEntity>().HasOne(x => x.Position)
                .WithMany(x => x.PositionInOrders)
                .HasForeignKey(x => x.PositionId);
            modelBuilder.Entity<PositionInOrderEntity>().HasOne(x => x.Order)
                .WithMany(x => x.PositionsInOrder)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<OrderEntity>().HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<OrderEntity>().HasOne(x => x.Table)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.TableId);
        }
    }
}
