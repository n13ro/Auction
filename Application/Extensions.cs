using Application.Services.LoginService;
using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            .RegisterServicesFromAssemblyContaining<UpdateUserDataCommandHandler>());

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<CreateLotCommandHandler>());

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<LoginCommandHandler>());

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<DepositOnBalanceCommandHandler>());

            return services;
        }
    }
}
