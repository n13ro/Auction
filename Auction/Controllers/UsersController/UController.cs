using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auction.Controllers.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("DepositOnBalance")]
        public async Task DepositOnBalance(DepositOnBalanceCommand cmd)
        {
            var userId = Convert
                .ToInt32(User
                .FindFirstValue(ClaimTypes.NameIdentifier));
            cmd.id = userId; 
            await _mediator.Send(cmd);
        }


        [Authorize]
        [HttpPost("CreateUser")]
        public async Task Create(CreateUserCommand cmd)
        {
            await _mediator.Send(cmd);
        }

        [HttpPut("{userId}")]
        public async Task UpdateUserData(int userId, UpdateUserDataCommand cmd)
        {

            cmd.Id = userId;
            await _mediator.Send(cmd);

        }

    }
}
