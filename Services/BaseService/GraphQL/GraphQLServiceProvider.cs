using System;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BaseService.GraphQL{
    public static class GraphQLServiceProvider{
        public static IServiceCollection AddGrapQLService(this IServiceCollection services,IWebHostEnvironment env){
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddScoped<ISchema, GraphQLSchema>();

            services.AddGraphQL((options, provider) =>
            {
                options.EnableMetrics = env.IsDevelopment();
                var logger = provider.GetRequiredService<ILogger<Startup>>();
                options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
            })
            .AddSystemTextJson()
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = env.IsDevelopment())
            // .AddGraphQLAuthorization(options =>
            // {
            //     options.AddPolicy(PolicyTypes.AuthorizedPolicy, p => p.RequireAuthenticatedUser());
            //     options.AddPolicy(PolicyTypes.AdminPolicy, p => p.RequireRole(RoleTypes.Admin));
            //     options.AddPolicy(PolicyTypes.UserPolicy, p => p.RequireRole(RoleTypes.User));
            //     options.AddPolicy(PolicyTypes.EmployeePolicy, p => p.RequireRole(RoleTypes.Employee));
            // })
            .AddGraphTypes(typeof(GraphQLSchema), ServiceLifetime.Scoped);

            return services;
        }
    }
}