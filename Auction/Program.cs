using Application;
using DotNetEnv;
using Infrastructure;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Registration;

namespace Auction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /// <summary>
            /// Loading variables from an .env file
            /// </summary>
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddAuthCustom(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAuthorization();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auction API",
                    Version = "v0.0.6",
                    Description = "API для управления аукционом",
                    Contact = new OpenApiContact
                    {
                        Name = "Auza Team",
                        Email = "auzateamind@gmail.com"
                    },
                });
            });


            builder.Logging.ClearProviders();
            builder.Logging.AddSimpleConsole(o =>
            {
                o.TimestampFormat = "HH:mm:ss";
                o.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(o => 
                    o.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0);
                app.UseSwaggerUI();
                
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
