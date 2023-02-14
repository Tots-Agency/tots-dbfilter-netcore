using System;
using System.Collections.Generic;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;
using Tots.DbFilter.Services;
using Tots.DbFilter.Wheres;
using Microsoft.EntityFrameworkCore;
using Tots.DbFilter.Extensions;
using System.Linq.Dynamic.Core;

namespace Tots.DbFilter
{
	public class TotsDbQuery<T> where T : class
    {
        protected TotsDbQueryService<T> _request;
        protected DbContext _context;

        public TotsDbQuery(DbContext context, TotsDbListRequest<T> request)
        {
            _context = context;
            this._request = new TotsDbQueryService<T>(request);
        }

        public void SetRequest(TotsDbListRequest<T> request)
        {
            this._request = new TotsDbQueryService<T>(request);
        }

        public TotsDbQueryService<T> GetQueryRequest()
        {
            return this._request;
        }

        public async Task<TotsDbListResponse<T>> Execute()
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet.IgnoreAutoIncludes<T>();

            // Process All Withs
            foreach (string with in this.GetQueryRequest().GetWiths())
            {
                query = query.Include(with);
            }

            // Process All Wheres
            foreach (AbstractWhere where in this.GetQueryRequest().GetWheres())
            {
                query = query.Where(where.ExecutePredicate<T>());
            }

            // Process All Groups
            List<T> result;
            int count;
            if (this.GetQueryRequest().GetGroups().Count() > 0)
            {
                result = query.GroupBy(PredicateBuilderExtension.GroupByExpressionList<T>(this.GetQueryRequest().GetGroups().ToArray()).Compile()).Select(g => g.First()).Skip((this._request!.GetPage() - 1) * this._request.GetPerPage()).Take(this._request.GetPerPage()).ToList();
                count = query.GroupBy(PredicateBuilderExtension.GroupByExpressionList<T>(this.GetQueryRequest().GetGroups().ToArray()).Compile()).Select(g => g.First()).Count();

                if (this.GetQueryRequest().GetSums().Count() > 0)
                {
                    var groupsBy = string.Join(",", this.GetQueryRequest().GetGroups().ToArray());
                    var resultGroups = query.GroupBy(groupsBy).Select("new (Key, " + ConvertedSumsToConditionString() + ", FirstOrDefault() as FirstItem)").ToDynamicList();
                    this.ProcessSumsColumns(result, resultGroups);
                }
            }
            else
            {
                result = query.Skip((this._request!.GetPage() - 1) * this._request.GetPerPage()).Take(this._request.GetPerPage()).ToList();
                count = await query.CountAsync();
            }

            return new TotsDbListResponse<T>
            {
                CurrentPage = this._request.GetPage(),
                PerPage = this._request.GetPerPage(),
                LastPage = int.Parse((count / this._request.GetPerPage()).ToString()),
                Data = result,
                Total = count
            };
        }

        protected string ConvertedSumsToConditionString()
        {
            string response = "";
            foreach (string sum in this.GetQueryRequest().GetSums())
            {
                if (response.Length > 0)
                {
                    response = response + ", ";
                }
                response = response + "Sum(it." + sum + ") AS " + sum + "Sum";
            }
            return response;
        }

        protected void ProcessSumsColumns(List<T> result, List<dynamic> resultGroups)
        {
            foreach(T item in result)
            {
                ProcessSumInItem(item, resultGroups);
            }
        }

        protected void ProcessSumInItem(T item, List<dynamic> resultGroups)
        {
            foreach(string sum in this.GetQueryRequest().GetSums())
            {
                string firstGroupBy = this.GetQueryRequest().GetGroups()[0];

                dynamic itemGroupValue = item.GetType().GetProperty(firstGroupBy).GetValue(item);

                //dynamic itemGrup = resultGroups.Find(x => x.FirstItem.Status == itemGroupValue); // Funciona

                dynamic itemGrup = GetResultItem(firstGroupBy, itemGroupValue, resultGroups);

                dynamic newValue = itemGrup.GetType().GetProperty(firstGroupBy + "Sum").GetValue(itemGrup);

                item.GetType().GetProperty(firstGroupBy).SetValue(item, newValue);
            }
        }

        protected dynamic GetResultItem(string key, dynamic value, List<dynamic> resultGroups)
        {
            foreach(dynamic item in resultGroups)
            {
                dynamic firstItem = item.GetType().GetProperty("FirstItem").GetValue(item);
                dynamic valueKey = firstItem.GetType().GetProperty(key).GetValue(firstItem);

                if (valueKey == value)
                {
                    return item;
                }
            }

            return null;
        }
    }
}

