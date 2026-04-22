using webapi.Application.UseCases.EntitiesResponses;

namespace webapi.Application.UseCases.GetMe
{
    public class GetUserByIdUCOutput
    {
        public UserResponse User { get; set; } = null!;
    }
}
