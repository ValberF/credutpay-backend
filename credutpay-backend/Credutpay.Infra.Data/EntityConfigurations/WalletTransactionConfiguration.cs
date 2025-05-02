using Credutpay.Domain.Registrations.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Credutpay.Infra.Data.EntityConfigurations
{
    public class WalletTransactionConfiguration : BaseEntityConfiguration<WalletTransaction>
    {
        public override void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Amount).IsRequired();

            builder.HasOne(w => w.WalletSender)
                .WithMany(s => s.SentTransactions)
                .HasForeignKey(w => w.WalletSenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.WalletReceiver)
                .WithMany(r => r.ReceiverTransactions)
                .HasForeignKey(w => w.WalletReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new WalletTransaction
                {
                    Id = "00000000-0000-0000-0000-000000000201",
                    WalletSenderId = "00000000-0000-0000-0000-000000000103", 
                    WalletReceiverId = "00000000-0000-0000-0000-000000000102",
                    Amount = 150.50m,
                    CreatedAt = DateTime.Parse("2025-04-27T00:00:00Z"),
                    IsDeleted = false
                },
                new WalletTransaction
                {
                    Id = "00000000-0000-0000-0000-000000000202",
                    WalletSenderId = "00000000-0000-0000-0000-000000000104",
                    WalletReceiverId = "00000000-0000-0000-0000-000000000102",
                    Amount = 75.25m,
                    CreatedAt = DateTime.Parse("2025-04-29T00:00:00Z"),
                    IsDeleted = false
                },
                new WalletTransaction
                {
                    Id = "00000000-0000-0000-0000-000000000203",
                    WalletSenderId = "00000000-0000-0000-0000-000000000103",
                    WalletReceiverId = "00000000-0000-0000-0000-000000000104",
                    Amount = 50.00m,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false
                }
            );
        }
    }
}
