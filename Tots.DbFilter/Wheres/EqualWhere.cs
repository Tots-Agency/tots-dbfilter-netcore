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
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());
            var value = this.GetValue();

            if (value == null)
            {
                return PredicateBuilderExtension.Equal<T>(key, null);
            }

            int valueInt;
            if (Int32.TryParse(value.ToString(), out valueInt))
            {
                return PredicateBuilderExtension.Equal<T>(key, valueInt);
            }

            if(value is bool){
                return PredicateBuilderExtension.Equal<T>(key, (bool)value);
            }

            return PredicateBuilderExtension.Equal<T>(key, value.ToString()!);
        }
    }
}

