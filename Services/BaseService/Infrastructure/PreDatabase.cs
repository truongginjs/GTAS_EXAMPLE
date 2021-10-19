using System;
using System.Linq;
using BaseService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BaseService.Infrastructure
{
    public static class PreDatabase
    {
        public static IApplicationBuilder SeedFakeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BaseContext>();
                context.SeedFakeData();
            }
            return app;
        }

        public static void SeedFakeData(this BaseContext context)
        {
            if (!context.Test.Any())
            {
                Console.WriteLine("====> Seeding fake data ...");
                context.AddRange(
                    new Test { Id = Guid.NewGuid(), Name = "Test 1", Description = "Des test 1"},
                    new Test { Id = Guid.NewGuid(), Name = "Test 2", Description = "Des test 2"},
                    new Test { Id = Guid.NewGuid(), Name = "Test 3", Description = "Des test 3"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("====> We already has data.");

            }
        }
    }
}