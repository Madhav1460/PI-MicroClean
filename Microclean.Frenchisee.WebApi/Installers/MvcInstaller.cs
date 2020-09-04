using FluentValidation.AspNetCore;
using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel;
using Microclean.Frenchisee.WebApi.Extensions;
using Microclean.Frenchisee.WebApi.Infrastructure;
using Microclean.Frenchisee.WebApi.Options;
using Microclean.ProviderLayer.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Microclean.Frenchisee.WebApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            var assemblyForFluentValidation = AppDomain.CurrentDomain.Load("Microclean.ProviderLayer");
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<ValidationFilter>();
            })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssembly(assemblyForFluentValidation));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(x =>
               {
                   x.SaveToken = true;
                   x.TokenValidationParameters = tokenValidationParameters;
               });

            services.AddSingleton(typeof(UserClameResponse));

            var assembly = AppDomain.CurrentDomain.Load("Microclean.ProviderLayer");
            string connection = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<MicrocleanDbContext>(options => options.UseMySQL(connection));
            services.AddMediatR(assembly);
            services.AddCoreAppMapper();
            services.AddProvider();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UserIdPipe<,>));
        }
    }
}
