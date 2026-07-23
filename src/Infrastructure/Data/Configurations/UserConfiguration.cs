using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Username)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Username)
               .IsUnique();

        builder.Property(x => x.PasswordHash)
               .IsRequired();
    }
}