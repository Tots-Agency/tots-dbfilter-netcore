using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class EqualWhere: AbstractWhere
	{
        public EqualWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_EQUAL;
            this._key = data.Key ?? "";
            this._value = data.Value ?? "";
            this._isDenied = data.IsDenied;
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());
            var value = this.GetValue();

            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return PredicateBuilderExtension.Equal<T>(key, null, _isDenied);
            }

            if (value is bool)
            {
                return PredicateBuilderExtension.Equal<T>(key, (bool)value, _isDenied);
            }

            int valueInt;
            if (!value.ToString().Contains(" ") && Int32.TryParse(value.ToString(), out valueInt))
            {
                return PredicateBuilderExtension.Equal<T>(key, valueInt, _isDenied);
            }

            return PredicateBuilderExtension.Equal<T>(key, value.ToString()!, _isDenied);
        }
    }
}

