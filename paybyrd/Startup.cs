using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using paybyrd.AutoMapper;
using paybyrd.Context;
using paybyrd.Entities;
using paybyrd.Entities.Request;
using paybyrd.Interfaces.Repository;
using paybyrd.Interfaces.Services;
using paybyrd.Services;
using System.Text.Json.Serialization;

namespace paybyrd
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
            services.AddAutoMapper(typeof(ConfigurationMapping));
            services.AddScoped<IDiffService, DiffService>();
            services.AddScoped<IDiffRepository, DiffRepository>();
            services.AddControllers();
            services.AddDbContext<PayByrdContext>(opt =>
             opt.UseInMemoryDatabase("PayByrdAvaliacao"));
            services.AddSwaggerGen(c =>
            {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "paybyrd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "paybyrd v1"));
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
