using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.AppServices;
using Application.Interfaces;
using Domain.Entities.UsersAgg;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore.AutoRegisterDi;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Recordatorio:
            /*
            
            Transitorio
            Los servicios de duración transitoria (AddTransient) se crean cada vez que el contenedor del servicio los solicita. Esta duración funciona mejor 
            para servicios sin estado ligeros.

            Con ámbito
            Los servicios de duración con ámbito (AddScoped) se crean una vez por solicitud del cliente (conexión).

            Singleton
            Los servicios con duración Singleton (AddSingleton) se crean la primera vez que se solicitan, o bien al ejecutar Startup.ConfigureServices y especificar 
            una instancia con el registro del servicio. Cada solicitud posterior usa la misma instancia. Si la aplicación requiere un comportamiento de singleton, 
            se recomienda permitir que el contenedor de servicios administre la duración del servicio. No implemente el patrón de diseño de singleton y proporcione 
            el código de usuario para administrar la duración del objeto en la clase.

             */

            //services.AddScoped<IUserAppService, UserAppService>();
            Assembly[] myAssemblies = System.Threading.Thread.GetDomain().GetAssemblies();
            // Forma dinámica con el injector de .Net Core (veo problematico tener que referenciar Infrastructure aunque solamente sea para esto)
            IList<Assembly> assemblies = new List<Assembly>
            {
                Assembly.Load(new AssemblyName("TortosaApi.Application")),
                Assembly.Load(new AssemblyName("TortosaApi.Domain"))/*,
                Assembly.Load(new AssemblyName("TortosaApi.Infrastructure"))*/
            };


            foreach (var assembly in assemblies.Where(x => x != null))
            {
                services.RegisterAssemblyPublicNonGenericClasses(assembly)
                    .Where(c => c.Name.EndsWith("AppService"))
                    .AsPublicImplementedInterfaces();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
