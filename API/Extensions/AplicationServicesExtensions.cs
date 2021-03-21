using API.Errors;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class AplicationServicesExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            //Interfaz Producto repositorio creado
            services.AddScoped<IproductRepository, ProductRepository>();
            //Interfaz Repositorio generico Creado
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            //configuracion del servicio //Bad request/five
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationError
                    {
                        Errors = errors
                    };//Bad request/five
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
