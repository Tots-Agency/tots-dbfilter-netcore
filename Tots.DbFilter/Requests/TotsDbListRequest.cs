using System;
using MediatR;
using Tots.DbFilter.Responses;

namespace Tots.DbFilter.Requests
{
	public class TotsDbListRequest<T>: IRequest<TotsDbListResponse<T>>
	{
        public string[]? With { get; set; }
        //public int? Limit { get; set; }
        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public string? Filters { get; set; }
        public string? FiltersString { get; set; }
        public string? Groups { get; set; }
        public string? Sums { get; set; }
    }
}

