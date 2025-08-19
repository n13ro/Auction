using Application.Services.LoginService;
using Application.Services.UserService.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Registration.JWT;
using Swashbuckle.AspNetCore.Annotations;


namespace Auction.Controllers.AuthController
{
    /// <summary>
    /// Контроллер для аутентификации и регистрации пользователей
    /// </summary>
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


        /// <summary>
        /// Регистрирует нового пользователя и возвращает JWT токен
        /// </summary>
        /// <param name="cmd">Данные для регистрации пользователя</param>
        /// <returns>JWT токен для аутентификации</returns>
        /// <response code="200">Пользователь успешно зарегистрирован и получен токен</response>
        /// <response code="400">Некорректные данные регистрации</response>
        /// <response code="409">Пользователь с таким email уже существует</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [AllowAnonymous]
        [HttpPost("SignIn")]
        [SwaggerOperation(
            Summary = "Регистрация нового пользователя",
            Description = "Регистрирует нового пользователя в системе и возвращает JWT токен для аутентификации. Email должен быть уникальным.",
            OperationId = "signIn",
            Tags = new[] { "Auth" })]
        [SwaggerResponse(200, "Пользователь успешно зарегистрирован и получен токен")]
        [SwaggerResponse(400, "Некорректные данные регистрации")]
        [SwaggerResponse(409, "Пользователь с таким email уже существует")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<ActionResult<string>> SignIn([FromBody] CreateUserCommand cmd)
        {
            var access = _jwtService.CreateToken(cmd.Id, cmd.Email, cmd.NickName);
            var (rt, exp) = _jwtService.CreateRefreshToken();
            await _mediator.Send(cmd);
            
            return Ok(new { accessToken = access, refreshToken = rt, expire = exp});
        }

        /// <summary>
        /// Аутентифицирует пользователя и возвращает JWT токен
        /// </summary>
        /// <param name="cmd">Данные для входа в систему</param>
        /// <returns>JWT токен для аутентификации</returns>
        /// <response code="200">Пользователь успешно аутентифицирован и получен токен</response>
        /// <response code="400">Некорректные данные входа</response>
        /// <response code="401">Неверный email или пароль</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [AllowAnonymous]
        [HttpPost("Login")]
        [SwaggerOperation(
            Summary = "Аутентификация пользователя",
            Description = "Аутентифицирует пользователя по email и паролю, возвращает JWT токен для доступа к защищенным ресурсам.",
            OperationId = "login",
            Tags = new[] { "Auth" })]
        [SwaggerResponse(200, "Пользователь успешно аутентифицирован и получен токен")]
        [SwaggerResponse(400, "Некорректные данные входа")]
        [SwaggerResponse(401, "Неверный email или пароль")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<ActionResult<string>> Login([FromBody] LoginWithTokenCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }

        /// <summary>
        /// Удаляет токен для выхода с аккаунта
        /// </summary>
        /// <response code="200">Пользователь вышел с аккаунта</response>
        /// <response code="400">Некорректные данные входа</response>
        /// <response code="401">Неверный email или пароль</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [AllowAnonymous]
        [HttpPost("Logout")]
        [SwaggerOperation(
            Summary = "Выход с аккаунта",
            Description = "Отзывает refresh токен пользователя.",
            OperationId = "logout",
            Tags = new[] { "Auth" })]
        [SwaggerResponse(200, "Пользователь вышел с аккаунта")]
        [SwaggerResponse(400, "Некорректные данные входа")]
        [SwaggerResponse(401, "Неверный email или пароль")]
        [SwaggerResponse(500, "Внутренняя ошибка сервера")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand cmd)
        {
            await _mediator.Send(cmd);
            return Ok();
        }

        /// <summary>
        /// Аутентифицирует пользователя и возвращает JWT c Refresh токеном
        /// </summary>
        /// <param name="cmd">Доп. данные для входа в систему</param>
        /// <returns>JWT токен для аутентификации</returns>
        /// <response code="200">Пользователь успешно аутентифицирован и получен токен</response>
        /// <response code="400">Некорректные данные входа</response>
        /// <response code="401">Неверный email или пароль</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ActionResult<string>> Refresh([FromBody] RefreshTokenCommand cmd)
        {
            return Ok(await _mediator.Send(cmd));
        }
    }
}
