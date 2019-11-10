using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SerjTm.Sample.TourSearcher.Common.Services;
using SerjTm.Sample.TourSearcher.Aggregator;
using SerjTm.Sample.TourSearcher.Imitator;

namespace SerjTm.Sample.TourSearcher.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(json => 
                {
                    json.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
