using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class NotInWhere : AbstractWhere
    {
        public NotInWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_NOT_IN;
            this._key = data.Key ?? "";
            this._value = data.Value ?? new List<dynamic>();
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());
            var value = this.GetValue();

            return PredicateBuilderExtension.NotContains<T>(key, value!);
        }
    }
}

