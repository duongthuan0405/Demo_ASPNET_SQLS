using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Application.UseCases.GetMe;

namespace webapi.WebAPI.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IGetUserByIdUC getUserByIdUC)
        {
            _getUserById = getUserByIdUC;
        }
        private readonly IGetUserByIdUC _getUserById;
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<GetUserByIdUCOutput>> GetMe()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var res = await _getUserById.ExecuteAsync(new GetUserByIdUCInput()
            {
                UserId = Guid.Parse(userId)
            });
            return Ok(res);
        }
    }
}
