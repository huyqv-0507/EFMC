using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Text;
using EFMC.Data.Common;
using EFMC.Data.Interfaces;
using EFMC.Data.Repositories;
using EFMC.Service.Common.Constants;
using EFMC.Service.Interfaces;
using EFMC.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EFMC.API.Common
{
    public class ServiceCollectionConf
    {
        public static void DI(IServiceCollection services)
        {
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Repository
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPharmacyRepository, PharmacyRepository>();
            services.AddTransient<IUserPharmacyRepository, UserPharmacyRepository>();
            services.AddTransient<IConsignmentRepository, ConsignmentRepository>();
            services.AddTransient<IConsignmentDrugRepository, ConsignmentDrugRepository>();
            services.AddTransient<IDrugRepository, DrugRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<IDrugIndustryRepository, DrugIndustryRepository>();
            services.AddTransient<IPharmacyIndustryRepository, PharmacyIndustryRepository>();
            #endregion

            #region Service
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPharmacyService, PharmacyService>();
            services.AddTransient<IConsignmentService, ConsignmentService>();
            services.AddTransient<IDrugService, DrugService>();
            #endregion

        }
        public static void Jwt(IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration[JwtConfConstant.ISSUER],
                        ValidAudience = configuration[JwtConfConstant.AUDIENCE],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JwtConfConstant.KEY])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
        }
        public static void Swagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EFMC APIs",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // Set the comments path for the Swagger JSON and UI.
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        public static void Cors(IServiceCollection services)
        {
            services.AddCors(option =>
                option.AddPolicy("MyCors", builder =>
                {
                    builder.WithOrigins(
                        "https://localhost:5001",
                        "https://localhost:8081")
                            .WithMethods("PUT", "DELETE", "GET", "POST");
                })
                ); ;
        }
    }
}
