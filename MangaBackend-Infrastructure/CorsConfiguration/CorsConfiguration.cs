using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MangaBackend_Infrastructure.CorsConfiguration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("") // your React app's origin
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            return services;
        }
    }
}
