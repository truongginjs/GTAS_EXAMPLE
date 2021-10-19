using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using SINNIKA.EFCore.Initialize.Types;
using Unidecode.NET;

namespace SINNIKA.EFCore.Initialize.Helpers
{
    internal static class Helper
    {
        public static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        public static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
        public static string UpperFirstLetter(this string str) => str.First().ToString().ToUpper() + str.Substring(1);

        public static Func<T, bool> GetFuncCondition<T>(string propertyName, Dictionary<string, object> conditions) where T : class
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var oneFilter = conditions.FirstOrDefault();
            if (!ConditionDict.tryGetConditionType(oneFilter.Key, out ConditionType condition))
                return _ => true;

            var valueCompareWith = oneFilter.Value;

            var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);

            Func<T, bool> result = x =>
            {
                var value = propertyInfo.GetValue(x);


                return true;
            };

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    result = x => DoCompare(condition,
                    Convert.ToBoolean(propertyInfo.GetValue(x)),
                    Convert.ToBoolean(valueCompareWith));
                    break;
                case TypeCode.Double:
                case TypeCode.Single:
                    result = x => DoCompare(condition,
                    Convert.ToSingle(propertyInfo.GetValue(x))
                    , Convert.ToSingle(valueCompareWith));
                    break;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    result = x => DoCompare(condition,
                    Convert.ToInt32(propertyInfo.GetValue(x)),
                    Convert.ToInt32(valueCompareWith));
                    break;
                case TypeCode.Char:
                    result = x => DoCompare(condition,
                    Convert.ToChar(propertyInfo.GetValue(x)),
                    Convert.ToChar(valueCompareWith));
                    break;
                case TypeCode.String:
                    result = x => DoCompare(condition,
                    Convert.ToString(propertyInfo.GetValue(x)),
                    Convert.ToString(valueCompareWith));
                    break;
                case TypeCode.Empty:
                default:
                    break;
            }
            return x => false;
        }

        public static bool DoCompare<TC>(ConditionType condition, TC value, TC other) where TC : IComparable
        {
            switch (condition)
            {
                case ConditionType.Equal:
                    return value.CompareTo(other) == 0;
                case ConditionType.NotEqual:
                    return value.CompareTo(other) != 0;
                case ConditionType.LessThan:
                    return value.CompareTo(other) < 0;
                case ConditionType.LessThanEqual:
                    return value.CompareTo(other) <= 0;
                case ConditionType.GreaterThan:
                    return value.CompareTo(other) > 0;
                case ConditionType.GreaterThanEqual:
                    return value.CompareTo(other) >= 0;
                default:
                    return true;
            }
        }

        public static bool DoCompare(ConditionType condition, string value, string other)
        {
            switch (condition)
            {
                case ConditionType.Equal:
                    return value.Equals(other);
                case ConditionType.NotEqual:
                    return !value.Equals(other);
                case ConditionType.Like:
                    return value.Contains(other);
                case ConditionType.NotLike:
                    return !value.Contains(other);
                case ConditionType.InsensitiveLike:
                    return value.ToLower().Contains(other.ToLower());
                case ConditionType.NotInsensitiveLike:
                    return !value.ToLower().Contains(other.ToLower());
                case ConditionType.InsensitiveUnidecodeLike:
                    return value.ToLower().Unidecode()
                    .Contains(other.ToLower().Unidecode());
                case ConditionType.NotInsensitiveUnidecodeLike:
                    return !value.ToLower().Unidecode()
                    .Contains(other.ToLower().Unidecode());
                case ConditionType.StartsWith:
                    return value.StartsWith(other);
                case ConditionType.EndsWith:
                    return value.EndsWith(other);
                case ConditionType.Regex:
                    return new Regex(other).IsMatch(value);
                default:
                    return true;
            }
        }
    }
}