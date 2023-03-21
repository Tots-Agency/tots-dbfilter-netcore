using System;
using System.Text.Json;
using Tots.DbFilter.Entities;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Wheres;

namespace Tots.DbFilter.Services
{
	public class TotsDbQueryService<T> where T : class
    {
        protected TotsDbListRequest<T> _request;
        protected int _page = 1;
        protected int _perPage = 50;
        protected List<AbstractWhere> _wheres = new List<AbstractWhere>();
        protected List<string> _groups = new List<string>();
        protected List<string> _sums = new List<string>();
        protected List<string> _withs = new List<string>();

        public TotsDbQueryService(TotsDbListRequest<T> request)
        {
            this._request = request;
            this.ProcessRequest();
        }

        protected void ProcessRequest()
        {
            this._page = this._request.Page ?? 1;
            this._perPage = this._request.PerPage ?? 50;
            this.ProcessData();
            this.ProcessDataString();
            this.ProcessGroups();
            this.ProcessSums();
            this.ProcessWiths();
        }

        public void AddWhere(AbstractWhere where)
        {
            _wheres.Add(where);
        }
        public void AddWhereEqual(string key, dynamic value)
		{
			WhereEntity data = new WhereEntity
			{
				Key = key,
				Value = value
			};
            _wheres.Add(new EqualWhere(data));
        }

        public void RemoveWhereByKey(string key)
        {
            foreach (AbstractWhere where in _wheres)
            {
                if (where.IsSameKey(key))
                {
                    _wheres.Remove(where);
                }
            }
        }

        protected void ProcessWheres(WhereEntity[] wheres)
        {
            foreach (var where in wheres)
            {
                if (where.Type == AbstractWhere.TYPE_EQUAL)
                {
                    _wheres.Add(new EqualWhere(where));
                }
                else if (where.Type == AbstractWhere.TYPE_IN)
                {
                    _wheres.Add(new InWhere(where));
                }
                else if (where.Type == AbstractWhere.TYPE_LIKE)
                {
                    _wheres.Add(new LikeWhere(where));
                }
                else if (where.Type == AbstractWhere.TYPE_LIKES)
                {
                    _wheres.Add(new LikesWhere(where));
                }
                else if (where.Type == AbstractWhere.TYPE_BETWEEN)
                {
                    _wheres.Add(new BetweenWhere(where));
                }
            }
        }

        protected void ProcessData()
        {
            if (this._request.Filters == null || this._request.Filters.Length == 0) return;

            try
            {
                string dataString = this.Base64Decode(this._request.Filters);
                var jsonObj = JsonSerializer.Deserialize<FiltersEntity>(dataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (jsonObj != null && jsonObj.Wheres != null)
                {
                    this.ProcessWheres(jsonObj.Wheres);
                }
            }
            catch
            {
                return;
            }
        }

        protected void ProcessDataString()
        {
            if (this._request.FiltersString == null || this._request.FiltersString.Length == 0) return;

            try
            {
                var jsonObj = JsonSerializer.Deserialize<FiltersEntity>(this._request.FiltersString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (jsonObj != null && jsonObj.Wheres != null)
                {
                    this.ProcessWheres(jsonObj.Wheres);
                }

            }
            catch
            {

            }
        }

        protected void ProcessGroups()
		{
            if (this._request.Groups == null || this._request.Groups.Length == 0) return;

			this._groups = this._request.Groups!.Split(",").ToList<string>();
        }

        protected void ProcessWiths()
		{
            if (this._request.Withs == null || this._request.Withs.Length == 0) return;

			this._withs = this._request.Withs!.Split(",").ToList<string>();
        }

        protected void ProcessSums()
		{
            if (this._request.Sums == null || this._request.Sums.Length == 0) return;

            this._sums = this._request.Sums!.Split(",").ToList<string>();
        }

        protected string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public int GetPage()
        {
            return this._page;
        }

        public int GetPerPage()
        {
            return this._perPage;
        }

        public List<AbstractWhere> GetWheres()
        {
            return this._wheres;
        }
        
        public List<string> GetGroups()
        {
            return this._groups;
        }

        public List<string> GetSums()
        {
            return this._sums;
        }

        public List<string> GetWiths()
        {
            return this._withs;
        }
    }
}