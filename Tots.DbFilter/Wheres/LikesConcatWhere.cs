using System;
using System.Linq.Expressions;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Extensions;

namespace Tots.DbFilter.Wheres
{
	public class LikesConcatWhere : AbstractWhere
    {
        protected List<string> _keys = new List<string>();
        public LikesConcatWhere(WhereEntity data)
        {
            this._type = AbstractWhere.TYPE_LIKESCONCAT;
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

            return PredicateBuilderExtension.LikesConcat<T>(keys, value.ToString()!);
        }

        public new bool IsSameKey(string key)
        {
            foreach (string keyInt in this._keys)
            {
                if (this.CleanKey(keyInt) == key)
                {
                    return true;
                }
            }

            return false;
        }

        public List<string> GetKeys()
        {
            return this._keys;
        }
    }
}

