using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infraestructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;//1 Configuracion
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        //public IConfiguration Configuration { get; } reemplazo 1

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                       //Auto Mapping
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            //Cadena de conexion
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            //Aplicacion servicio extension
            services.AddAplicationServices();
            //Aplicacion Swagger
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();//usamos excepcion           
            
            //en producion lo eliminamos
            //if (env.IsDevelopment())
            //{
            //    //app.UseDeveloperExceptionPage();Excepción Eliminamos
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            //}

            app.UseStatusCodePagesWithReExecute("/errors/{0}");//redirecciona a error controller

            app.UseHttpsRedirection();

            app.UseRouting();
            //usar archivos estaticos para las imagenes
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
