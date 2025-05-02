using Credutpay.Domain.Core.Enums;
using Credutpay.Domain.Registrations.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credutpay.Infra.Data.EntityConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(u => u.Wallet)
                .WithOne(w => w.User)
                .HasForeignKey<Wallet>(w => w.UserId);

            builder.HasData(
                new User
                {
                    Id = "00000000-0000-0000-0000-000000000001",
                    Name = "Administrador",
                    Email = "admin@credutpay.com",
                    Password = "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", // Senha123!
                    Type = UserType.User,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false,
                    WalletId = "00000000-0000-0000-0000-000000000101"
                },
                new User
                {
                    Id = "00000000-0000-0000-0000-000000000002",
                    Name = "Lojista Silva",
                    Email = "lojista@credutpay.com",
                    Password = "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", // Senha123!
                    Type = UserType.User,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false,
                    WalletId = "00000000-0000-0000-0000-000000000102"
                },
                new User
                {
                    Id = "00000000-0000-0000-0000-000000000003",
                    Name = "João Usuário",
                    Email = "joao@credutpay.com",
                    Password = "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", // Senha123!
                    Type = UserType.User,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false,
                    WalletId = "00000000-0000-0000-0000-000000000103"
                },
                new User
                {
                    Id = "00000000-0000-0000-0000-000000000004",
                    Name = "Maria Usuária",
                    Email = "maria@credutpay.com",
                    Password = "$2a$11$jOsKulVMar93/ay9GWM6COq.nC3DaEWHQ775T6SOfXj/C70AToWgK", // Senha123!
                    Type = UserType.User,
                    CreatedAt = DateTime.Parse("2025-05-01T00:00:00Z"),
                    IsDeleted = false,
                    WalletId = "00000000-0000-0000-0000-000000000104"
                }
            );
        }
    }
}
