using Credutpay.Domain.Registrations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credutpay.Infra.Data.EntityConfigurations
{
    public class WalletConfiguration : BaseEntityConfiguration<Wallet>
    {
        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Amount).IsRequired();

            builder.HasMany(s => s.SentTransactions)
              .WithOne(w => w.WalletSender)
              .HasForeignKey(w => w.WalletSenderId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.ReceiverTransactions)
              .WithOne(w => w.WalletReceiver)
              .HasForeignKey(w => w.WalletReceiverId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Wallet
                {
                    Id = "00000000-0000-0000-0000-000000000101",
                    UserId = "00000000-0000-0000-0000-000000000001",
                    Amount = 5000.00m,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false
                },
                new Wallet
                {
                    Id = "00000000-0000-0000-0000-000000000102",
                    UserId = "00000000-0000-0000-0000-000000000002",
                    Amount = 10000.00m,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false
                },
                new Wallet
                {
                    Id = "00000000-0000-0000-0000-000000000103",
                    UserId = "00000000-0000-0000-0000-000000000003",
                    Amount = 1200.50m,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false
                },
                new Wallet
                {
                    Id = "00000000-0000-0000-0000-000000000104",
                    UserId = "00000000-0000-0000-0000-000000000004",
                    Amount = 850.75m,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false
                }
            );
        }
    }
}
