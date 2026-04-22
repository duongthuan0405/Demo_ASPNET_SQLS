namespace webapi.Infrastructure.Database.Models
{
    public class MessageDBE
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;

        public UserDBE Sender { get; set; } = null!;

    }
}
