using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task Create(CreateUserCommand cmd)
        {
            await _mediator.Send(cmd);
        }


    }
}
