using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Auction.Controllers.UsersController
{
    /// <summary>
    /// Контроллер для управления пользователями (версия 1)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserV1Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserV1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Пополняет баланс текущего пользователя
        /// </summary>
        /// <param name="cmd">Команда пополнения баланса с указанием суммы</param>
        /// <returns>Результат операции пополнения</returns>
        /// <response code="200">Баланс успешно пополнен</response>
        /// <response code="400">Некорректная сумма пополнения</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [Authorize]
        [HttpPost("v1/depositOnBalance")]
        [SwaggerOperation(
            Summary = "Пополнение баланса пользователя",
            Description = "Пополняет баланс текущего авторизованного пользователя на указанную сумму. Сумма должна быть положительной и не превышать лимит.",
            OperationId = "depositOnBalance",
            Tags = new[] { "UserV1" })]
        [SwaggerResponse(200, "Баланс успешно пополнен")]
        [SwaggerResponse(400, "Некорректная сумма пополнения")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task DepositOnBalic(DepositOnBalanceCommand cmd)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            cmd.Id = userId;
            await _mediator.Send(cmd);

        }

        //[Authorize]
        //[HttpPost("createUser")]
        //[SwaggerOperation(
        //    Summary = "Создание пользователя. Только для АДМИНОВ!",
        //    Description = "Создание пользователя. Только для АДМИНОВ!",
        //    OperationId = "createUser",
        //    Tags = new[] { "UserV1" })]
        //[SwaggerResponse(200, "Вы пополнили баланс успешно")]
        //[SwaggerResponse(401, "Вы не авторизованы")]
        //[SwaggerResponse(500, "Ошибка сервера")]
        //public async Task Create(CreateUserCommand cmd)
        //{
        //    await _mediator.Send(cmd);
        //}


        /// <summary>
        /// Обновляет данные профиля текущего пользователя
        /// </summary>
        /// <param name="cmd">Команда обновления данных пользователя</param>
        /// <returns>Результат обновления профиля</returns>
        /// <response code="200">Данные профиля успешно обновлены</response>
        /// <response code="400">Некорректные данные для обновления</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [Authorize]
        [HttpPut("v1/editProfile")]
        [SwaggerOperation(
            Summary = "Обновление профиля пользователя",
            Description = "Обновляет данные профиля текущего авторизованного пользователя. Можно изменить никнейм, email и пароль.",
            OperationId = "editProfile",
            Tags = new[] { "UserV1" })]
        [SwaggerResponse(200, "Данные профиля успешно обновлены")]
        [SwaggerResponse(400, "Некорректные данные для обновления")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task UpdateUserData(UpdateUserDataCommand cmd)
        {
            await _mediator.Send(cmd);

        }

        /// <summary>
        /// Закрывает лот, принадлежащий текущему пользователю
        /// </summary>
        /// <param name="cmd">Команда закрытия лота</param>
        /// <returns>Результат закрытия лота</returns>
        /// <response code="200">Лот успешно закрыт</response>
        /// <response code="400">Некорректные данные для закрытия лота</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <response code="403">Нет прав для закрытия этого лота</response>
        /// <response code="404">Лот не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [Authorize]
        [HttpPost("v1/closeLot")]
        [SwaggerOperation(
            Summary = "Закрытие лота пользователем",
            Description = "Закрывает лот, принадлежащий текущему авторизованному пользователю. Только владелец лота может его закрыть.",
            OperationId = "closeLot",
            Tags = new[] { "UserV1" })]
        [SwaggerResponse(200, "Лот успешно закрыт")]
        [SwaggerResponse(400, "Некорректные данные для закрытия лота")]
        [SwaggerResponse(401, "Пользователь не авторизован")]
        [SwaggerResponse(403, "Нет прав для закрытия этого лота")]
        [SwaggerResponse(404, "Лот не найден")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task CloseLot(CloseLotCommand cmd)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            cmd.userId = userId;
            await _mediator.Send(cmd);
        }

    }
}
