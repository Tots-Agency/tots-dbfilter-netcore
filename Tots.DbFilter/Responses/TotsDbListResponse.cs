using System;
namespace Tots.DbFilter.Responses
{
	public class TotsDbListResponse<T>
	{
        public int? CurrentPage { get; set; }
        public List<T>? Data { get; set; }
        public string? FirstPageUrl { get; set; }
        public int? From { get; set; }
        public int? LastPage { get; set; }
        public string? LastPageUrl { get; set; }
        public string? NextPageUrl { get; set; }
        public string? Path { get; set; }
        public int? PerPage { get; set; }
        public string? PrevPageUrl { get; set; }
        public int? To { get; set; }
        public int? Total { get; set; }
    }
}

