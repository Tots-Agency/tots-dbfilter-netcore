using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class BetweenDateWhere : AbstractWhere
    {

        protected dynamic? _from;
        protected dynamic? _to;
        public BetweenDateWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_BETWEEN_DATE;
            this._key = data.Key ?? "";
            this._from = data.From ?? 0;
            this._to = data.To ?? 0;
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());

            return PredicateBuilderExtension.BetweenDates<T>(key, DateTime.Parse(this._from?.ToString()), DateTime.Parse(this._to?.ToString()));
        }
    }
}

