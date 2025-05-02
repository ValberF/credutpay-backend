using Credutpay.Domain.Registrations.Entities;
using Credutpay.Infra.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Credutpay.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new WalletConfiguration());
            builder.ApplyConfiguration(new WalletTransactionConfiguration());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletTransaction> WalletTransaction { get; set; }

    }
}
