using System;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace BaseService.GraphQL{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider services):base(services)
        {
            Query = services.GetRequiredService<GraphQLQuery>();
            Mutation = services.GetRequiredService<GraphQLMutation>();
            Subscription = services.GetRequiredService<GraphQLSubscription>();
            // RegisterTypeMapping(typeof(Category), typeof(CategoryGraphType));
        }
    }
}