using Microsoft.AspNetCore.Authentication.JwtBearer;
using DiplomaProject.Helpers;
using DiplomaProject.Repository;
using DiplomaProject.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using DiplomaProject.Secrets;
using AutoMapper;
using DiplomaProject.AutoMapper;
using DiplomaProject.Extensions;
using DiplomaProject.DatabaseSecret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DiplomaProject.Helpers.RoleHelpers;
using Microsoft.AspNetCore.Authorization;
using DiplomaProject.EntityModels.Enums;

namespace DiplomaProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DiplomaProject", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AppSecret.AuthSecret.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AppSecret.AuthSecret.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AppSecret.AuthSecret.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization(opts => {
                opts.AddPolicy("Admin", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "Role" && c.Value == UserRole.Admin.ToString())));

                opts.AddPolicy("BaseUser", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "Role" && c.Value == UserRole.BaseUser.ToString())));

                opts.AddPolicy("PremiumUser", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "Role" && c.Value == UserRole.PremiumUser.ToString())));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            builder.Services.ConfigureDataAccessServices((o) => o.CollectSqlQueries = false);

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IAuthHelper, AuthHelper>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IProfilePhotoService, ProfilePhotoService>();
            builder.Services.AddScoped<IProfilePhotoRepository, ProfilePhotoRepository>();
            builder.Services.AddScoped<IHealthParametrRepository, HealthParametrRepository>();
            builder.Services.AddScoped<IHealthParametrService, HealthParametrService>();
            builder.Services.AddScoped<IUserBMIRepository, UserBMIRepository>();
            builder.Services.AddScoped<IGoalService, GoalService>();
            builder.Services.AddScoped<IGoalRepository, GoalRepository>();
            builder.Services.AddScoped<IBMICalculationService, BMICalculationService>();
            builder.Services.AddScoped<ICalorieService, CalorieService>();
            builder.Services.AddScoped<IGoalTemplateRepository, GoalTemplateRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();

            //try
            //{
            //    using var scope = app.Services.CreateScope();
            //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            //    {
            //        context.Database.MigrateAsync().Wait();
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            app.Run();
        }

        
    }
}