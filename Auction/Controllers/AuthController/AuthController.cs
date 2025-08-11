using Application.Services.LoginService;
using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.JWT;
using System.Threading.Tasks;
using static Auction.Controllers.Test.TestController;

namespace Auction.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IMediator _mediator;
        public AuthController(IJWTService jwtService, IMediator mediator)
        {
            _jwtService = jwtService;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult<string>> SignIn([FromBody] CreateUserCommand cmd)
        {
            await _mediator.Send(cmd);
            var access = _jwtService.CreateToken(cmd.Id, cmd.Email, cmd.NickName);
            return Ok(access);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand cmd)
        {
            var (id, email, nick) = await _mediator.Send(cmd);
            var access = _jwtService.CreateToken(id, email, nick);
            return Ok(access);
        }
    }
}
