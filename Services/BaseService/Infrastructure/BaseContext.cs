using BaseService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaseService.Infrastructure
{
    public class BaseContext : DbContext
    {
        public DbSet<Test> Test { get; set; }
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasData(
                new Test { Id = Guid.Parse("897cc639-0563-4a4d-b3a2-ce9be9c60144"), Name = "Test 1", Description = "Sub 1" },
                new Test { Id = Guid.Parse("897cc639-0563-4a4d-b3a2-ce9be9c60145"), Name = "Test 2", Description = "Sub 2" },
                new Test { Id = Guid.Parse("897cc639-0563-4a4d-b3a2-ce9be9c60146"), Name = "Test 3", Description = "Sub 3" });

        }
    }
}