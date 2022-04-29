using System;
using CrudApi.Authentication;
using CrudApi.GenericControllerAttribute;
using CrudApi.Repository;
using InfoLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CrudApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger<Logger>("LogConfig.xml");
            
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });
            
            // добавил возможность bearer аутентификации, т.к. метод расширения для этого писал давно в своих других приложениях.
            // использование ее по умолчанию убрал,
            // но достаточно всего лишь убрать закомментированные атрибуты и компоненты конвеера запроса
            services.AddJwtSigningAuthentication<SigningSymmetricKey>("we123qso33oa943ja443ia9h439aj55ofo35");
            
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connection));

            services.AddCors();
            
            services.
                AddControllers(o => o.Conventions.Add(
                    new GenericControllerRouteConvention() // соотношения маршрутов из атрибутов к контроллерам
                )).
                ConfigureApplicationPartManager(m => 
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider() // добавление типизированных контроллеров
                    ));
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrudApi", Version = "v1" }); });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudApi v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            // по необходимости можно и ограничть кросс-доменные операции или вовсе отключить
            app.UseCors(builder => builder.AllowAnyOrigin()); 

            // app.UseAuthentication();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}