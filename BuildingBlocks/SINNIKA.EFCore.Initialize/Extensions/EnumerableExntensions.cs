using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SINNIKA.EFCore.Initialize.Helpers;
using SINNIKA.EFCore.Initialize.Types;

namespace SINNIKA.EFCore.Initialize.Extensions
{
    public static class EnumerableExntensions
    {
        public static IEnumerable<T> OrderByWithArguments<T>(this IEnumerable<T> query, Dictionary<string, OrderByType> orderBy) where T : class
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

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = Helper.GetPropertyInfo(typeof(T), name);
            var expr = Helper.GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = Helper.GetPropertyInfo(typeof(T), name);
            var expr = Helper.GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }


        public static IEnumerable<T> WhereAsyncWithArguments<T>(this IEnumerable<T> query, Dictionary<string, object> args) where T : class
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
                            var temp = data.WhereAsyncWithArguments<T>(c as Dictionary<string, object>);
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
                            var temp = data.WhereAsyncWithArguments(c as Dictionary<string, object>);
                            if (temp.Count() < 1) continue;
                            dataOr = temp.Union(dataOr);
                        }
                        data = data.Union(dataOr);
                    }
                }
                else
                {
                    Func<T, bool> funcCondition = Helper.GetFuncCondition<T>(name, value as Dictionary<string, object>);
                    data = (IEnumerable<T>)data.Where(funcCondition);
                }
            }

            return data;
        }
        }

}