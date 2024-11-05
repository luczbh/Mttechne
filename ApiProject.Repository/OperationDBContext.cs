using ApiProject.Entities.DB;
using ApiProject.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Repository
{
    public class OperationDBContext : DbContext, IApiProjectContext
    {
        public OperationDBContext(DbContextOptions<OperationDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Balance> Balance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClientId).HasName("PK_Client");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK_Product");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.SellerId).HasName("PK_Seller");
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.BalanceId).HasName("PK_Balance");

                entity.Property(e => e.TotalDebits).HasPrecision(18, 2);
                entity.Property(e => e.TotalCredits).HasPrecision(18, 2);

                entity.HasOne(d => d.Client)
                  .WithMany(p => p.Balances)
                  .HasForeignKey(d => d.ClientId)
                  .HasConstraintName("FK_Client_Balance");

                entity.HasOne(d => d.Product)
                   .WithMany(p => p.Balances)
                   .HasForeignKey(d => d.ProductId)
                   .HasConstraintName("FK_Product_Balance");

                entity.HasOne(d => d.Seller)
                  .WithMany(p => p.Balances)
                  .HasForeignKey(d => d.SellerId)
                  .HasConstraintName("FK_Seller_Balance");

            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.OperationId).HasName("PK_Operation");
                entity.Property(e => e.OperationDate).IsRequired().HasColumnType("datetime");

                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.SellerId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Value).HasPrecision(18, 2);

                entity.HasOne(d => d.Client)
                   .WithMany(p => p.Operations)
                   .HasForeignKey(d => d.ClientId)
                   .HasConstraintName("FK_Client_Operation");

                entity.HasOne(d => d.Product)
                   .WithMany(p => p.Operations)
                   .HasForeignKey(d => d.ProductId)
                   .HasConstraintName("FK_Product_Operation");

                entity.HasOne(d => d.Seller)
                  .WithMany(p => p.Operations)
                  .HasForeignKey(d => d.SellerId)
                  .HasConstraintName("FK_Seller_Operation");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

