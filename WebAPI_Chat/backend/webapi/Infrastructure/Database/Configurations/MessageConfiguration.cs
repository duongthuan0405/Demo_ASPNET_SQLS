using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Infrastructure.Database.Models;

namespace webapi.Infrastructure.Database.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageDBE>
    {
        public void Configure(EntityTypeBuilder<MessageDBE> builder)
        {
            builder.ToTable("MESSAGE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                   .HasColumnName("Content")   
                   .HasColumnType("NVARCHAR(MAX)");

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .HasColumnType("DATETIME");

            builder.Property(x => x.UpdatedAt)
                   .HasColumnName("UpdatedAt")
                   .HasColumnType("DATETIME");

            builder.HasOne(x => x.Sender)
                   .WithMany(sender => sender.Messages)
                   .HasForeignKey(x => x.UserId)
                   .HasConstraintName("FK_Message_User");
        }
    }
}
