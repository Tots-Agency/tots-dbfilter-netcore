using System;
namespace Tots.DbFilter.Responses
{
	public class TotsDbListResponse<T>
	{
        public int? Current_page { get; set; }
        public List<T>? Data { get; set; }
        public string? First_page_url { get; set; }
        public int? From { get; set; }
        public int? Last_page { get; set; }
        public string? Last_page_url { get; set; }
        public string? Next_page_url { get; set; }
        public string? Path { get; set; }
        public int? Per_page { get; set; }
        public string? Prev_page_url { get; set; }
        public int? To { get; set; }
        public int? Total { get; set; }
    }
}

