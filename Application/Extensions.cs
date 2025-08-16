using Application.Services.BidService.Command;
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
            #region USER COMMANDS

            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<CreateUserCommandHandler>());
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<UpdateUserDataCommandHandler>());
            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<CloseLotCommandHandler>());
            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<DepositOnBalanceCommandHandler>());

            #endregion

            #region LOT COMMANDS

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<CreateLotCommandHandler>());

            #endregion

            #region AUTH COMMANDS

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<LoginCommandHandler>());

            #endregion

            #region BID COMMANDS

            services.AddMediatR(cfg => cfg
           .RegisterServicesFromAssemblyContaining<BidPlacingCommandHandler>());

            #endregion

            return services;
        }
    }
}
