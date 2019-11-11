using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SerjTm.Sample.TourSearcher.Aggregator;
using SerjTm.Sample.TourSearcher.Common.Services;
using SerjTm.Sample.TourSearcher.Imitator;

namespace SerjTm.Sample.TourSearcher.WebApi3
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
            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerDocument();

            var imitationDict = ImitatorService.ImitateDict();

            services.AddSingleton<TuiProvider.Storages.MemoryDictStorage>(new TuiProvider.Storages.MemoryDictStorage(imitationDict));
            services.AddScoped<IDictService, TuiProvider.Services.MemoryDictService>();

            services.AddSingleton<TuiProvider.Storages.MemoryTourStorage>(
                new TuiProvider.Storages.MemoryTourStorage(ImitatorService.ImitateTours("Tui", imitationDict)));
            services.AddScoped<ISearchService, TuiProvider.Services.MemorySearchService>();

            services.AddSingleton<OtherProvider.Storages.MemoryTourStorage>(
                new OtherProvider.Storages.MemoryTourStorage(ImitatorService.ImitateTours("Other", imitationDict)));
            services.AddScoped<ISearchService, OtherProvider.Services.MemorySearchService>();

            services.AddScoped<AggregatorService, AggregatorService>();

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

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
