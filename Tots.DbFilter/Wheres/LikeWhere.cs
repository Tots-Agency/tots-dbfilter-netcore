using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class LikeWhere : AbstractWhere
    {
        public LikeWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_LIKE;
            this._key = data.Key ?? "";
            this._value = data.Value ?? "";
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());
            var value = this.GetValue();

            return PredicateBuilderExtension.Like<T>(key, value!.ToString()!);
        }
    }
}

