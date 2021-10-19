using System.Linq;
using BaseService.GraphQL.GraphType;
using BaseService.Infrastructure;
using BaseService.Infrastructure.Repositories;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using SINNIKA.EFCore.Initialize.Extensions;

namespace BaseService.GraphQL.Queries
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(ITestRepository repo)
        {
            FieldAsync<ListGraphType<TestGraphType>>("tests",
            arguments: new QueryArguments(),
            resolve: async context =>
            {
                var ListpropertyName = context.SubFields.Keys.ToArray();
                var result = repo.InvokeAsync(table => table.SelectMembers(ListpropertyName).ToListAsync());
                return await result;

            });
        }
    }
}
