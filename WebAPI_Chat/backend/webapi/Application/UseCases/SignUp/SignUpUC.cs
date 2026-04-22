using webapi.Application.BusinessExceptions;
using webapi.Application.Repositories;
using webapi.Application.Services;
using webapi.Entities;

namespace webapi.Application.UseCases.SignUp
{
    public class SignUpUC : ISignUpUC
    {
        private IUserRepository _userRepository;
        private IUnitOfWorks _unitOfWorks;
        public SignUpUC(IUserRepository userRepository, IUnitOfWorks unitOfWorks)
        {
            _userRepository = userRepository;
            _unitOfWorks = unitOfWorks;
        }
        public async Task<SignUpUCOutput> ExecuteAsync(SignUpUCInput input)
        {
            var errors = input.Validate();
            if (errors != null)
            {
                throw new InvalidUseCasesInputException("Invalid input", errors);
            }

            var newUser = new User
            {
                FullName = input.FullName,
                Username = input.Username,
                Password = input.Password
            };
            User? user = await _userRepository.GetByUsernameAsync(input.Username);
            if(user != null)
            {
                throw new ConflictUniqueValueException("Username already exists", new()
                {
                    { "Username", new() {"Username already exists" } }
                });
            }
            Guid userId = await _userRepository.AddAsync(newUser);
            await _unitOfWorks.FinishTransaction();
            return new SignUpUCOutput
            {
                IsSuccess = true,
                UserId = userId
            };
        }
    }
}
