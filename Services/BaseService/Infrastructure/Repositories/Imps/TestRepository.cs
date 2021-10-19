using BaseService.Models;
using BaseService.Infrastructure.Repositories;
using SINNIKA.EFCore.Initialize;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BaseService.Infrastructure.Repositories.Imps
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        private readonly DbSet<Test> _table;
        public TestRepository(BaseContext context) : base(context)
        {
            _table = context.Test;
        }
       
    }
}