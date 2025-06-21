using Franco.Sentry.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Franco.Sentry.Infra.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Username)
            .HasColumnType("text")
            .IsRequired();
    }
}