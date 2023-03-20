using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class BetweenWhere : AbstractWhere
    {

        protected dynamic? _from;
        protected dynamic? _to;
        public BetweenWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_BETWEEN;
            this._key = data.Key ?? "";
            this._from = data.From ?? 0;
            this._to = data.To ?? 0;
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            string key = this.CleanKey(this.GetKey());

            return PredicateBuilderExtension.Between<T>(key, this._from, this._to);
        }
    }
}

