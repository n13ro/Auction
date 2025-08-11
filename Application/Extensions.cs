using Application.Services.LoginService;
using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services) //IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<CreateUserCommandHandler>());

            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<UpdateDataUserCommandHandler>());

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<CreateLotCommandHandler>());

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<LoginCommandHandler>());

            return services;
        }
    }
}
