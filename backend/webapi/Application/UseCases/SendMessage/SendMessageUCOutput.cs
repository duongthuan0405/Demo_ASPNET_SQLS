namespace webapi.Application.UseCases.SendMessage
{
    public class SendMessageUCOutput
    {
        public Guid MessageId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    }
}
