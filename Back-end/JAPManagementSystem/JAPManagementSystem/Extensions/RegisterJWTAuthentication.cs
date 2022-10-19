﻿using JAPManagementSystem.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JAPManagementSystem.Extensions
{
    public static class RegisterJWTAuthentication
    {
        public static void RegisterAuthentication(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddIdentityCore<IdentityUser>(setupAction =>
            {
                setupAction.User.AllowedUserNameCharacters = config.GetSection("Authentication:AllowedUserNameCharacters").Value;
                setupAction.User.RequireUniqueEmail = true;
                setupAction.Password.RequireDigit = false;
                setupAction.Password.RequiredUniqueChars = 2;
                setupAction.Password.RequireLowercase = false;
                setupAction.Password.RequireNonAlphanumeric = false;
                setupAction.Password.RequireUppercase = false;
                setupAction.Password.RequiredLength = 5;
                setupAction.SignIn.RequireConfirmedEmail = false;
                setupAction.SignIn.RequireConfirmedPhoneNumber = false;
            });
            new IdentityBuilder(typeof(IdentityUser), typeof(IdentityRole), service)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            var jwtSettings = config.GetSection("JwtSettings");
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(jwtSettings.GetSection("SecurityKey").Value))
                };
            });
        }
    }
}
