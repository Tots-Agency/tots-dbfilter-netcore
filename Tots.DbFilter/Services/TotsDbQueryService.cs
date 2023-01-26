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
        }

        protected void ProcessWheres(WhereEntity[] wheres)
        {
            foreach (var where in wheres)
            {
                if (where.Type == AbstractWhere.TYPE_EQUAL)
                {
                    _wheres.Add(new EqualWhere(where));
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
    }
}

