using System;
namespace Tots.DbFilter.Entities
{
	public class WhereEntity
	{
        public string? Type { get; set; }
        public string? Key { get; set; }
        public dynamic? Value { get; set; }
        public List<string>? Keys { get; set; }
        public dynamic? From { get; set; }
        public dynamic? To { get; set; }
    }
}

