using MediatR;
using PubsDBFirst;
using Tots.DbFilter;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;

namespace Example.Handlers.Pubs
{
    public class SaleListRequest : TotsDbListRequest<Sale>
    {

    }

    public class SaleListHandler : IRequestHandler<SaleListRequest, TotsDbListResponse<Sale>>
    {
        PubsContext _context;
        public SaleListHandler(PubsContext context)
        {
            _context = context;
        }

        public Task<TotsDbListResponse<Sale>> Handle(SaleListRequest request, CancellationToken cancellationToken)
        {
            TotsDbQuery<Sale> query = new TotsDbQuery<Sale>(_context, request);

            return query.Execute();
        }
    }
}
