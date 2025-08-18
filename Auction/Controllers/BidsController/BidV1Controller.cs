using Application.Services.BidService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Auction.Controllers.BidsController
{
    /// <summary>
    /// Контроллер для управления ставками на аукционе (версия 1)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class BidV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public BidV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Размещает ставку на указанный лот
        /// </summary>
        /// <param name="cmd">Данные для размещения ставки</param>
        /// <returns>Результат размещения ставки</returns>
        /// <response code="200">Ставка успешно размещена</response>
        /// <response code="400">Некорректная сумма ставки или недостаточно средств</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="404">Лот не найден</response>
        /// <response code="409">Лот уже закрыт или неактивен</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [Authorize]
        [HttpPost("v1/placeBid")]
        [SwaggerOperation(
            Summary = "Размещение ставки на лот",
            Description = "Размещает ставку на указанный лот. Ставка должна быть больше минимальной ставки лота и текущей максимальной ставки. У пользователя должно быть достаточно средств на балансе.",
            OperationId = "placeBid",
            Tags = new[] { "BidV1" })]
        [SwaggerResponse(200, "Ставка успешно размещена")]
        [SwaggerResponse(400, "Некорректная сумма ставки или недостаточно средств")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(404, "Лот не найден")]
        [SwaggerResponse(409, "Лот уже закрыт или неактивен")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task PlaceBid([FromBody] BidPlacingCommand cmd)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int UserId = Convert.ToInt32(userIdString);
            cmd.userId = UserId;
            await _mediator.Send(cmd);

        }
    }
}
