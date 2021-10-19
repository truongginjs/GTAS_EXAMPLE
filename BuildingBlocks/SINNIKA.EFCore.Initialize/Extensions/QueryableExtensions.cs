using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using SINNIKA.EFCore.Initialize.Helpers;
using SINNIKA.EFCore.Initialize.Types;
using Unidecode.NET;

namespace SINNIKA.EFCore.Initialize.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SelectMembers<T>(this IQueryable<T> source, params string[] memberNames)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var bindings = memberNames
                .Select(name => Expression.PropertyOrField(parameter, name))
                .Select(member => Expression.Bind(member.Member, member));
            var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var selector = Expression.Lambda<Func<T, T>>(body, parameter);
            return source.Select(selector);
        }

        public static IQueryable<T> OrderByWithArguments<T>(this IQueryable<T> query, Dictionary<string, OrderByType> orderBy) where T : class
        {
            var data = query;
            foreach (var field in orderBy.Keys)
            {
                var feildUpperFirstLetter = field.UpperFirstLetter();
                try
                {
                    data = orderBy[field] == OrderByType.ASC ? data.OrderBy(feildUpperFirstLetter)
                                    : data.OrderByDescending(feildUpperFirstLetter);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return data;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = Helper.GetPropertyInfo(typeof(T), name);
            var expr = Helper.GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = Helper.GetPropertyInfo(typeof(T), name);
            var expr = Helper.GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }


        public static IQueryable<T> WhereWithArguments<T>(this IQueryable<T> query, Dictionary<string, object> args) where T : class
        {
            var data = query;
            foreach (var arg in args)
            {
                var name = arg.Key;
                var value = arg.Value;

                if (ConditionDict.tryGetConditionType(name, out ConditionType condition))
                {
                    if (condition.Equals(ConditionType.And))
                    {
                        IEnumerable<T> dataAnd = null;
                        var ls = value as List<object>;
                        foreach (var c in ls)
                        {
                            var temp = data.WhereWithArguments<T>(c as Dictionary<string, object>);
                            if (temp.Count() < 1) break;
                            dataAnd = dataAnd == null ? temp : temp.Intersect(dataAnd);
                        }
                        data = data.Intersect(dataAnd);
                    }
                    else if (condition.Equals(ConditionType.Or))
                    {
                        IEnumerable<T> dataOr = null;
                        var ls = value as List<object>;
                        foreach (var c in ls)
                        {
                            var temp = data.WhereWithArguments(c as Dictionary<string, object>);
                            if (temp.Count() < 1) continue;
                            dataOr = temp.Union(dataOr);
                        }
                        data = data.Union(dataOr);
                    }
                }
                else
                {
                    Func<T, bool> funcCondition = Helper.GetFuncCondition<T>(name, value as Dictionary<string, object>);
                    data = (IQueryable<T>)data.Where(funcCondition);
                }
            }

            return data;
        }
    }
}