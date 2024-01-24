using System;
using System.Linq.Expressions;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	abstract public class AbstractWhere
	{
        public static string TYPE_LIKES = "likes";
        public static string TYPE_EQUAL = "equal";
        public static string TYPE_NOTEQUAL = "notequal";
        public static string TYPE_IN = "in";
        public static string TYPE_LIKE = "like";
        public static string TYPE_BETWEEN = "between";
        public static string TYPE_BETWEEN_DATE = "between_date";

        protected string _type = "";
        protected string _key = "";
        protected dynamic? _value;

        abstract public Expression<Func<T, bool>> ExecutePredicate<T>();

        public string CleanKey(string key)
        {
            return key.FirstCharToUpper().Replace(" ", "").Replace(";", "");
        }

        public bool IsSameKey(string key)
        {
            if (this._key == key)
            {
                return true;
            }

            return false;
        }

        public string GetTypeWhere()
        {
            return this._type;
        }

        public string GetKey()
        {
            return this._key;
        }

        public object? GetValue()
        {
            return this._value;
        }
    }
}

