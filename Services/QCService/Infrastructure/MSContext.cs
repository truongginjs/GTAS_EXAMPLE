using BaseService.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseService.Infrastructure
{
    public class MSContext : DbContext
    {
        public DbSet<Test> Test { get; set; }
        public DbSet<QC> QC { get; set; }
        public DbSet<DefectLabrary> DefectLabrary { get; set; }
        public DbSet<QCZone> QCZone { get; set; }
        public DbSet<AQLLibrary> AQLLibrary { get; set; }

        public MSContext(DbContextOptions<MSContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}