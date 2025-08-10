using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.JWT;

namespace Auction.Controllers.Test
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IJWTService _jwtService;

        public TestController(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        public record LoginReq(int userId, string email, string nickName);

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginReq req)
        {
            var access = _jwtService.CreateToken(req.userId, req.email, req.nickName);
            return Ok(access);
        }

        [Authorize]
        [HttpGet("hui")]
        public string GetStr()
        {
            return "sosi hyi";
        }
    }
}
