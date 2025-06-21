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

        builder.Property(c => c.Id)
            .HasColumnName("id");
            
        builder.Property(c => c.Username)
            .HasColumnName("username")
            .HasColumnType("varchar(255)")
            .IsRequired();
        
        builder.Property(c => c.Password)
            .HasColumnName("password")
            .HasColumnType("text")
            .IsRequired();
        
        builder.Property(c => c.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(255)")
            .IsRequired();
        
        builder.Property(c => c.Document)
            .HasColumnName("document")
            .HasColumnType("varchar(20)")
            .IsRequired();
        
        builder.Property(c => c.Phone)
            .HasColumnName("phone")
            .HasColumnType("varchar(25)")
            .IsRequired();
        
        builder.Property(c => c.Status)
            .HasColumnName("statu")
            .HasColumnType("boolean")
            .IsRequired();
        
        builder.Property(c => c.CreatedAt)
            .HasColumnName("createdAt")
            .HasColumnType("timestamp")
            .IsRequired();
    }
}