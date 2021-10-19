using System.Collections.Generic;
using System.Linq;

namespace SINNIKA.EFCore.Initialize.Types{
    internal static class ConditionDict
    {
        public static readonly string Offset = "offset";
        public static readonly string Limit = "limit";
        public static readonly string Where = "where";
        public static readonly string OrderBy = "orderBy";

        public static readonly Dictionary<ConditionType, string> Operators = new Dictionary<ConditionType, string>() {
            {ConditionType.Not,"_not"},
            {ConditionType.Equal,"_eq"},
            {ConditionType.NotEqual,"_neq"},
            {ConditionType.GreaterThan,"_gt"},
            {ConditionType.GreaterThanEqual,"_gte"},
            {ConditionType.LessThan,"_lt"},
            {ConditionType.LessThanEqual,"_lte"},

            {ConditionType.Like,"_like"},
            {ConditionType.NotLike,"_nlike"},
            {ConditionType.InsensitiveLike,"_ilike"},
            {ConditionType.NotInsensitiveLike,"_nilike"},
            {ConditionType.InsensitiveUnidecodeLike,"_iulike"},
            {ConditionType.NotInsensitiveUnidecodeLike,"_niulike"},
            {ConditionType.StartsWith,"_startwith"},
            {ConditionType.EndsWith,"_endwith"},
            {ConditionType.Regex,"_regex"},

            {ConditionType.And,"_and"},
            {ConditionType.Or,"_or"},
        };

        public static ConditionType GetConditionType(string text) => Operators.SingleOrDefault(x => text.Equals(x.Value)).Key;

        public static bool tryGetConditionType(string text, out ConditionType type){
             if(Operators.ContainsValue(text)){
                 type = GetConditionType(text);
                 return true;
             }
             type = ConditionType.Unknown;
             return false;
        }

    }
}