using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auction.Controllers.LotsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("createLot")]
        public async Task Create(CreateLotCommand cmd)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int UserId = Convert.ToInt32(userIdString);
            await _mediator.Send(new CreateLotCommand
            {
                userId = UserId,
                Name = cmd.Name,
                Description = cmd.Description,
                StartingPrice = cmd.StartingPrice,
                MinBet = cmd.MinBet,
                IsExtraTime = cmd.IsExtraTime,
                LotLife = cmd.LotLife
            });
        }

        
    }
}
