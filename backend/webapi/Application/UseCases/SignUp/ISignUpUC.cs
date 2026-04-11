
using webapi.Application.BusinessExceptions;
using webapi.Application.Repositories;
using webapi.Application.Services.UnitOfWorks;
using webapi.Application.UseCases.Base;
using webapi.Entities;

namespace webapi.Application.UseCases.SignUp
{
    public interface ISignUpUC : IUseCase<SignUpUCInput, SignUpUCOutput>
    {
        
    }
}
