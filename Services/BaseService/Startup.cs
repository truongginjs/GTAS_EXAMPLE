using BaseService.GraphQL;
using BaseService.gRPC;
using BaseService.Infrastructure;
using BaseService.Infrastructure.Repositories;
using BaseService.Infrastructure.Repositories.Imps;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BaseService
{
    public class Startup
    {
        private IWebHostEnvironment _env { get; }

        public Startup(IWebHostEnvironment  env)
        {
            _env=env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.WriteIndented = true);

            services.AddDbContext<BaseContext>(option => option.UseInMemoryDatabase("InMemory"));


            services.AddGrpc();
            AddServices(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGrpcService<TestService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            app.SeedFakeDatabase();
        }


        private IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddScoped<ITestRepository,TestRepository>();

            services.AddGrapQLService(_env);

            return services;
        }
    }
}
