﻿using System;
using System.Collections.Generic;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;
using Tots.DbFilter.Services;
using Tots.DbFilter.Wheres;
using Microsoft.EntityFrameworkCore;
using Tots.DbFilter.Extensions;

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
            }
            else
            {
                result = query.Skip((this._request!.GetPage() - 1) * this._request.GetPerPage()).Take(this._request.GetPerPage()).ToList();
                count = await query.CountAsync();
            }

            return new TotsDbListResponse<T>
            {
                PerPage = this._request.GetPerPage(),
                Data = result,
                Total = count
            };
        }
    }
}

