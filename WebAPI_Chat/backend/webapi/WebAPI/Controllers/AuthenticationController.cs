using Microsoft.AspNetCore.Mvc;
using webapi.Application.BusinessExceptions;
using webapi.Application.UseCases.SignIn;
using webapi.Application.UseCases.SignUp;

[ApiController]
[Route("/api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ISignInUC _signInUC;
    private readonly ISignUpUC _signUpUC;

    public AuthenticationController(
        ISignInUC signInUC,
        ISignUpUC signUpUC)
    {
        _signInUC = signInUC;
        _signUpUC = signUpUC;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInUCOutput>> SignIn(SignInUCInput input)
    {
        var result = await _signInUC.ExecuteAsync(input);
        return Ok(result);
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<SignUpUCOutput>> SignUp(SignUpUCInput input)
    {
        var result = await _signUpUC.ExecuteAsync(input);
        return Ok(result);
    }
}