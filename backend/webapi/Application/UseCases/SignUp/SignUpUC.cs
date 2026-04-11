using webapi.Application.BusinessExceptions;
using webapi.Application.Repositories;
using webapi.Application.Services.UnitOfWorks;
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

            var user = new User
            {
                FullName = input.FullName,
                UserName = input.Username,
                Password = input.Password
            };

            Guid userId = await _userRepository.AddAsync(user);
            await _unitOfWorks.FinishTransaction();
            return new SignUpUCOutput
            {
                IsSuccess = true,
                UserId = userId
            };
        }
    }
}
