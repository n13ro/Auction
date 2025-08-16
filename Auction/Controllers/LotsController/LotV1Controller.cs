using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Auction.Controllers.LotsController
{
    /// <summary>
    /// Контроллер для управления лотами аукциона (версия 1)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LotV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        public LotV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создает новый лот для аукциона
        /// </summary>
        /// <param name="cmd">Данные для создания лота</param>
        /// <returns>Результат создания лота</returns>
        /// <response code="200">Лот успешно создан</response>
        /// <response code="400">Некорректные данные лота</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [Authorize]
        [HttpPost("createLot")]
        [SwaggerOperation(
            Summary = "Создание нового лота",
            Description = "Создает новый лот для аукциона с указанными параметрами. Пользователь должен быть авторизован. Лот автоматически становится активным после создания.",
            OperationId = "createLot",
            Tags = new[] { "LotV1" })]
        [SwaggerResponse(200, "Лот успешно создан")]
        [SwaggerResponse(400, "Некорректные данные лота")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
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
