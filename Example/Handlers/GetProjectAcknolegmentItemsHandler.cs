using Example.Entities;
using MediatR;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;
using Tots.DbFilter;
using Example.Persistence;

namespace Example.Handlers
{

    public class GetProjectAcknolegmentItemsRequest : IRequest<TotsDbListResponse<ProjectAcknolegmentItem>>
    {
        public TotsDbListRequest<ProjectAcknolegmentItem> Request { get; set; }
    }

    public class GetProjectAcknolegmentItemsHandler : IRequestHandler<GetProjectAcknolegmentItemsRequest, TotsDbListResponse<ProjectAcknolegmentItem>>
    {
        GreenwayContext _context;
        private TotsDbQuery<ProjectAcknolegmentItem> dbFilter;

        public GetProjectAcknolegmentItemsHandler(GreenwayContext context)
        {
            _context = context;
        }

        public async Task<TotsDbListResponse<ProjectAcknolegmentItem>> Handle(GetProjectAcknolegmentItemsRequest request, CancellationToken cancellationToken)
        {
            dbFilter = new TotsDbQuery<ProjectAcknolegmentItem>(_context, request.Request);
            return await dbFilter.Execute();
        }

        public IQueryable<ProjectAcknolegmentItem> ExtraQuery(IQueryable<ProjectAcknolegmentItem> query)
        {
            var where = dbFilter.GetQueryRequest().GetWhereByKey("Acknolegment.InstallId");
            if (where == null)
            {
                return query;
            }

            dbFilter.GetQueryRequest().RemoveWhereByObject(where);

            int valueInt;
            Int32.TryParse(where.GetValue().ToString(), out valueInt);

            return query.Where(p => p.Acknolegment.InstallId == valueInt);
        }
    }
}
