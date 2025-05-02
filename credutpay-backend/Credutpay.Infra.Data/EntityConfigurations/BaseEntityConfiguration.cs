using Credutpay.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Credutpay.Infra.Data.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnType("character varying(36)")
                .IsRequired();

            builder.Property(e => e.IsDeleted)
                .HasColumnType("boolean") 
                .HasDefaultValue(false)   
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp")
                .IsRequired(false);
        }
    }
}
