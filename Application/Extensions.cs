using Application.Services.BidService.Command;
using Application.Services.LoginService;
using Application.Services.LotService.Command;
using Application.Services.UserService.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services) 
        {
            
            #region AuthCommands
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<LoginCommandHandler>());
            #endregion

            #region UserCommands
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<CreateUserCommandHandler>());
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<UpdateDataUserCommandHandler>());
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<DepositOnBalanceCommandHandler>());
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<CloseLotCommandHandler>());
            #endregion

            #region LotCommands
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<CreateLotCommandHandler>());
            #endregion

            #region BidCommands
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<BidPlacingCommandHandler>());
            #endregion


            // services.AddMediatR(cfg => cfg
            //.RegisterServicesFromAssemblyContaining<>());

            // services.AddMediatR(cfg => cfg
            //.RegisterServicesFromAssemblyContaining<>());

            // services.AddMediatR(cfg => cfg
            //.RegisterServicesFromAssemblyContaining<>());

            return services;
        }
    }
}
