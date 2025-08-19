using Application.Services.LotService.Command;
using Application.Services.LotService.DTOs;
using Application.Services.LotService.Queries;
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
        /// Получите все лоты аукциона
        /// </summary>
        /// <param name="cmd">Данные для создания лота</param>
        /// <returns>Результат создания лота</returns>
        /// <response code="200">Лоты успешно получены</response>
        /// <response code="400">Некорректные данные лотов</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet("v1/getAll")]
        [SwaggerOperation(
            Summary = "Получение всех лотов",
            Description = "Получите все лоты аукциона. Пользователь не обязан быть авторизован.",
            OperationId = "createLot",
            Tags = new[] { "LotV1" })]
        [SwaggerResponse(200, "Лот успешно создан")]
        [SwaggerResponse(400, "Некорректные данные лота")]
        //[SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<IEnumerable<LotResponse>> GetAll()
        {
            var all = await _mediator.Send(new GetAllLotsQuery());
            return all;
            
        }

        /// <summary>
        /// Получите один лот аукциона
        /// </summary>
        /// <param name="lotId">Id для получения лота</param>
        /// <returns>Результат создания лота</returns>
        /// <response code="200">Лот успешно получен</response>
        /// <response code="400">Некорректные данные лота</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet("v1/{lotId}")]
        [SwaggerOperation(
            Summary = "Получение лота",
            Description = "Получите лот аукциона. Пользователь не обязан быть авторизован",
            OperationId = "getByIdLot",
            Tags = new[] { "LotV1" })]
        [SwaggerResponse(200, "Лот успешно получен")]
        [SwaggerResponse(400, "Некорректные данные лота")]
        //[SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<IEnumerable<LotResponse>> GetByLot(int lotId)
        {
            var all = await _mediator.Send();
            return all;

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
        [HttpPost("v1/createLot")]
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
