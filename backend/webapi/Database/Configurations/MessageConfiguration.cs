using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Database.Models;

namespace webapi.Database.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
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

            builder.HasOne<User>(x => x.Sender)
                   .WithMany(sender => sender.Messages)
                   .HasForeignKey(x => x.UserId)
                   .HasConstraintName("FK_Message_User");
        }
    }
}
