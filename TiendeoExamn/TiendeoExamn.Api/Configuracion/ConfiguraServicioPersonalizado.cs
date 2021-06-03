using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendeoExamn.Api.Logica.Aplicacion;
using TiendeoExamn.Api.Logica.Dto;

namespace TiendeoExamn.Api.Configuracion
{
    public static class ConfiguraServicioPersonalizado
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            AddRegisterServices(services);
            AddRegisterRepositorios(services);

            return services;
        }

        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRealizarVuelo, RealizarVuelo>();
            return services;
        }

        private static IServiceCollection AddRegisterRepositorios(IServiceCollection services)
        {

            return services;
        }
    }
}
