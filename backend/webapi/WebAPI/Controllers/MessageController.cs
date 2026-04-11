using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Application.UseCases.GetAllMessages;
using webapi.Application.UseCases.SendMessage;
using webapi.WebAPI.DTOs;

namespace webapi.WebAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ISendMessageUC _sendMessageUC;
        private readonly IGetAllMessagesUC _getAllMessagesUC;
        public MessageController(ISendMessageUC sendMessageUC, IGetAllMessagesUC getAllMessageUC)
        {
            _sendMessageUC = sendMessageUC;
            _getAllMessagesUC = getAllMessageUC;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetAllMessagesUCOutput>> GetAllMessages()
        {
            var res = await _getAllMessagesUC.ExecuteAsync(new GetAllMessagesUCInput());
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SendMessageUCOutput>> SendMessage(SendMessageRequest request)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            SendMessageUCInput input = new()
            {
                SenderId = Guid.Parse(userId),
                Content = request.Content
            };
            var res = await _sendMessageUC.ExecuteAsync(input);
            return Ok(res);
        }
    }
}
