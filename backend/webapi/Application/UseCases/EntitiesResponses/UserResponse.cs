namespace webapi.Application.UseCases.EntitiesResponses
{
    public class UserResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
