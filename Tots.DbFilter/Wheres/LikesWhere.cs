using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class LikesWhere : AbstractWhere
    {
        protected List<string> _keys = new List<string>();
        public LikesWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_LIKES;
            this._keys = data.Keys ?? new List<string>();
            this._value = data.Value ?? "";
        }

        public override Expression<Func<T, bool>> ExecutePredicate<T>()
        {
            var value = this.GetValue();

            List<string> keys = new List<string>();

            foreach (string keyInt in this._keys)
            {
                string key = this.CleanKey(keyInt);
                keys.Add(key);
            }

            return PredicateBuilderExtension.Likes<T>(keys, value.ToString()!);
        }
    }
}

