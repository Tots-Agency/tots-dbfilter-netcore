﻿using System;
namespace Tots.DbFilter.Entities
{
	public class FiltersEntity
	{
        public WhereEntity[]? Wheres { get; set; }
        public OrderEntity[]? Orders { get; set; }
    }
}