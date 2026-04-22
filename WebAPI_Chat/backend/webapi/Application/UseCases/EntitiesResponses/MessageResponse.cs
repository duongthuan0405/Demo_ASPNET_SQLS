namespace webapi.Application.UseCases.EntitiesResponses
{
    public class MessageResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = null;
        public Guid SenderId { get; set; } = Guid.Empty;

        public UserResponse Sender { get; set; } = null!;
    }
}
