namespace webapi.Infrastructure.Database.Models
{
    public class UserDBE
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;   
        public List<MessageDBE> Messages { get; set; } = new List<MessageDBE>();
    }
}
