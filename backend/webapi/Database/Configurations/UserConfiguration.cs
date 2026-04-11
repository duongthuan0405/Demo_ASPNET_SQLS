using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Database.Models;

namespace webapi.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("Username")
                   .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Password)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("Password")
                   .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.FullName)
                   .HasMaxLength(100)
                   .HasColumnName("FullName")
                   .HasColumnType("NVARCHAR(100)");
        }
    }
}
