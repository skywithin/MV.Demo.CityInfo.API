using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MV.Demo.CityInfo.API.Contexts;
using MV.Demo.CityInfo.API.Services;

namespace MV.Demo.CityInfo.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(o =>
                    {
                        o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); //Support XML output format e.g. header: Accept application/xml
                    });

            services.AddTransient<IMailService, LocalMailService>();

            var connectionString = _configuration["connestionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");

                //app.UseExceptionHandler("/error").WithConventions(x => {
                //    x.ContentType = "application/json";
                //    x.MessageFormatter(s => JsonConvert.SerializeObject(new
                //    {
                //        Message = "Internal Server Error"
                //    }));
                //    x.OnError((exception, httpContext) =>
                //    {
                //        Log.Error(exception, "WebAPI Global Exception Handler");
                //        return Task.CompletedTask;
                //    });
                //});
            }

            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
