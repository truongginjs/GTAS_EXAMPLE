using BaseService.GraphQL.Queries;
using BaseService.Infrastructure;
using GraphQL.Types;

namespace BaseService.GraphQL
{
    public class GraphQLQuery : ObjectGraphType
    {
        public GraphQLQuery(BaseContext context)
        {

            Field<TestQuery>(nameof(TestQuery), resolve: context => new { });
        }
    }
}