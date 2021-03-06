using JMusik.Data;
using JMusik.Data.Contratos;
using JMusik.Data.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JMusik.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IProductoRepositorio, ProductoRepositorio>();
            services.AddTransient<IOrdenRepositorio, OrdenRepositorio>();

            //services.AddScoped<IOrdenRepositorio>((serviceProvider) =>
            //{
            //    var context = serviceProvider.GetService<TiendaDbContext>();
            //    var logger = serviceProvider.GetService<ILogger<OrdenRepositorio>>();
            //    return new OrdenRepositorio(context, logger);
            //});

            //services.AddScoped<IProductoRepositorio>((serviceProvider) =>
            //    {
            //        var context = serviceProvider.GetService<TiendaDbContext>();
            //        var logger = serviceProvider.GetService<ILogger<ProductoRepositorio>>();
            //        return new ProductoRepositorio(context, logger);
            //    }
            //);    

            services.AddDbContext<TiendaDbContext>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CString"));

            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

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
