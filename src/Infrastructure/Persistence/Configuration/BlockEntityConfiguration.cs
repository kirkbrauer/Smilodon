using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smilodon.Domain.Models;

namespace Smilodon.Infrastructure.Persistence.Configuration;

public class BlockEntityConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable("blocks");
        
        builder.HasKey(e => e.Id).HasName("blocks_pkey");

        builder.HasIndex(e => new { e.AccountId, e.TargetAccountId })
            .HasDatabaseName("index_blocks_on_account_id_and_target_account_id")
            .IsUnique();

        builder.HasIndex(e => e.TargetAccountId).HasDatabaseName("index_blocks_on_target_account_id");

        builder.Property(e => e.Id).HasColumnName("id");

        builder.Property(e => e.AccountId).HasColumnName("account_id");

        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");

        builder.Property(e => e.TargetAccountId).HasColumnName("target_account_id");

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("updated_at");

        builder.Property(e => e.Uri)
            .HasColumnType("character varying")
            .HasColumnName("uri");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.BlockAccounts)
            .HasForeignKey(d => d.AccountId)
            .HasConstraintName("fk_4269e03e65");

        builder.HasOne(d => d.TargetAccount)
            .WithMany(p => p.BlockTargetAccounts)
            .HasForeignKey(d => d.TargetAccountId)
            .HasConstraintName("fk_9571bfabc1");
    }
}