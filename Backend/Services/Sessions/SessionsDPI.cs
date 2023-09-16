using Core.IServices.Sessions;
using Core.IServices.Users;
using Microsoft.Extensions.DependencyInjection;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Sessions
{
    public static class SessionsDPI
    {
        public static IServiceCollection AddSessionService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SessionsMapper));
            services.AddTransient<ISessionService, SessionsService>();

            return services;
        }


    }
}
