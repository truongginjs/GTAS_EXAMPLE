using BaseService.Models;
using BaseService.Infrastructure.Repositories;
using SINNIKA.EFCore.Initialize;

namespace BaseService.Infrastructure.Repositories.Imps
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(BaseServiceContext context) : base(context)
        {
        }
    }
}