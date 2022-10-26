﻿using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.API.Extensions
{
    public static class RegisterDBContextExtension
    {
        public static void RegisterDBContext(this IServiceCollection service, ConfigurationManager config)
        {
            service.AddDbContext<DataContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
        }
    }
}
