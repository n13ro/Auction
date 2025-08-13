using Application.Services.BidService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auction.Controllers.BidsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task PlaceBid([FromBody] BidPlacingCommand cmd)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int UserId = Convert.ToInt32(userIdString);
            cmd.userId = UserId;
            await _mediator.Send(cmd);

        }
    }
}
