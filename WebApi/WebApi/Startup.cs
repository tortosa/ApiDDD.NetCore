using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TortosaApi.Infrastructure.Repository.Entities.UsersAgg;

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

            // TODO @Tortosa : Automatizar los contextos
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            // Recordatorio:
            /*
            
            Transitorio
            Los servicios de duraci�n transitoria (AddTransient) se crean cada vez que el contenedor del servicio los solicita. Esta duraci�n funciona mejor 
            para servicios sin estado ligeros.

            Con �mbito
            Los servicios de duraci�n con �mbito (AddScoped) se crean una vez por solicitud del cliente (conexi�n).

            Singleton
            Los servicios con duraci�n Singleton (AddSingleton) se crean la primera vez que se solicitan, o bien al ejecutar Startup.ConfigureServices y especificar 
            una instancia con el registro del servicio. Cada solicitud posterior usa la misma instancia. Si la aplicaci�n requiere un comportamiento de singleton, 
            se recomienda permitir que el contenedor de servicios administre la duraci�n del servicio. No implemente el patr�n de dise�o de singleton y proporcione 
            el c�digo de usuario para administrar la duraci�n del objeto en la clase.

             */

            // Forma din�mica con el injector de .Net Core
            var assemblies = new List<Assembly>
            {
                Assembly.Load(new AssemblyName("TortosaApi.Application")),
                Assembly.Load(new AssemblyName("TortosaApi.Domain")),
                Assembly.Load(new AssemblyName("TortosaApi.Infrastructure"))
            };

            var allCandidates = new List<TypeInfo>();

            foreach (var assembly in assemblies)
            {
                var candidates = assembly.DefinedTypes.Where(c => c.IsInterface || c.IsClass);
                allCandidates.AddRange(candidates);
            }

            foreach (var interfaceCandidate in allCandidates.Where(c => c.IsInterface).Distinct())
            {
                var implementation = allCandidates.Where(t => t.ImplementedInterfaces.Contains(interfaceCandidate)).FirstOrDefault();
                if (interfaceCandidate != null && implementation != null)
                    services.AddScoped(interfaceCandidate, implementation);
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