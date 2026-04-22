
using webapi.Application.BusinessExceptions;
using webapi.Application.Repositories;
using webapi.Entities;

namespace webapi.Application.UseCases.GetMe
{
    public class GetUserByIdUC : IGetUserByIdUC
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdUC(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUserByIdUCOutput> ExecuteAsync(GetUserByIdUCInput input)
        {
            User? u = await _userRepository.GetByIdAsync(input.UserId);
            if (u == null)
            {
                throw new NotFoundException("User not found", null);
            }
            return new GetUserByIdUCOutput
            {
                User = new()
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Username = u.Username,
                }
            };
        }
    }
}
