using CollectionsAndLinq.WebApi.Extentions;
using CollectionsAndLinq.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace CollectionsAndLinq.WebApi
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
            services.AddDbContext<CollectionsAndLinqContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("CollectionsAndLinqDatabase")));

            services.AddAutoMapper();

            services.AddCustomServices();

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
                endpoint.MapControllers());
        }
    }
}
