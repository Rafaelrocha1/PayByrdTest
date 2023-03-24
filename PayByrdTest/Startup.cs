using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using paybyrd.AutoMapper;
using paybyrd.Context;
using paybyrd.Interfaces.Repository;
using paybyrd.Interfaces.Services;
using paybyrd.Services;

namespace paybyrdTest
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
        }
    }
}
