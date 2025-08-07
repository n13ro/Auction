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
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<CreateUserCommandHandler>());

            //services.AddMediatR(cfg => 
            //cfg.RegisterServicesFromAssemblyContaining<>());

            //services.AddMediatR(cfg => 
            //cfg.RegisterServicesFromAssemblyContaining<>());

            return services;
        }
    }
}
