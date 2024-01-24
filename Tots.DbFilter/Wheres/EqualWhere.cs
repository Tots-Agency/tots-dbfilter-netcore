using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class EqualWhere: AbstractWhere
	{
        private bool _isDenied;
        public EqualWhere(WhereEntity data, bool isDenied = false)
        {
            this._type = !isDenied ? AbstractWhere.TYPE_EQUAL : AbstractWhere.TYPE_NOTEQUAL;
            this._key = data.Key ?? "";
            this._value = data.Value ?? "";
            this._isDenied = isDenied;
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

