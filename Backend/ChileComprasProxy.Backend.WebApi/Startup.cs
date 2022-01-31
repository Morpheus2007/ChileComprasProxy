using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChileComprasProxy.Backend.Interfaces.Dto;
using ChileComprasProxy.Backend.Interfaces.Interface;
using ChileComprasProxy.Backend.ServiceProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ChileComprasProxy.Backend.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private IHostingEnvironment CurrentEnvironment { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {

            Configuration = configuration;
            CurrentEnvironment = env;
           

        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Proxy ChileCompras Covid Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "ChileComprasProxyApi.xml"));

                c.CustomSchemaIds(x => x.FullName);
            });

            //services proxies 
            services.AddTransient<ICountryProxy, CountryProxy>();
            services.AddCors(o => o.AddPolicy("DevelopmentPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }
            ));

            services.AddCors(o => o.AddPolicy("ProductionPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }
            ));


            //Mapeo DTO
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProxyProfile>();
            });

            services.AddSingleton(config.CreateMapper());

          
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

           
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Proxy ChileCompras Covid Api");
                });

                app.UseCors("DevelopmentPolicy");

                app.UseCors(builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

           

            app.UseMvc();
        }
    }
}
