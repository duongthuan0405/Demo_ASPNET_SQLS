
using webapi.Application.BusinessExceptions;
using webapi.Application.Repositories;
using webapi.Application.Services.Jwt;
using webapi.Entities;

namespace webapi.Application.UseCases.SignIn
{
    public class SignInUC : ISignInUC
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public SignInUC(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<SignInUCOutput> ExecuteAsync(SignInUCInput input)
        {
            // 1. Validate input
            var errors = input.Validate();
            if (errors != null && errors.Any())
            {
                throw new InvalidUseCasesInputException("Invalid input", errors);
            }

            // 2. Tìm user
            User? user = await _userRepository.GetByUsernameAsync(input.Username);

            // 3. Check tồn tại & password
            if (user == null || user.Password != input.Password)
            {
                throw new InvalidUseCasesInputException(
                    "Username or password is incorrect",
                    null);
            }

            string jwtToken = _jwtService.GenerateToken(user);

            // 4. Map output
            return new SignInUCOutput
            {
                UserId = user.Id,
                Username = user.UserName,
                FullName = user.FullName,
                Token = jwtToken
            };
        }
    }
}
